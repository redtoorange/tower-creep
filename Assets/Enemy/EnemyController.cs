using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Enemy.EnemyControllerEvents;
using TowerCreep.Enemy.Resources.Waves;
using TowerCreep.Map;

namespace TowerCreep.Enemy
{
    public class EnemyController : Node2D
    {
        public static Action<EnemyControllerEvent> OnEnemyControllerEvent;

        [Export] private float initialWait = 10.0f;
        [Export] private List<EnemyWaveData> enemyWaveDefs;

        // Spawning Logic
        private EnemyWaveData currentWaveDef;
        private float spawnCooldown;
        private int numberSpawned;
        private MobSpawnerState spawnerState = MobSpawnerState.NotStarted;
        private int currentWave = 0;
        [Export] private float waveCooldown = 10.0f;
        private float waveCooldownElapsed = 0.0f;

        // Enemy Pathing
        [Export] private NodePath mobMovementRoutePath;
        private Line2D mobMovementRoute;

        // Enemy Tracking
        private List<Enemy> enemies;

        public override void _Ready()
        {
            mobMovementRoute = GetNode<Line2D>(mobMovementRoutePath);

            enemies = new List<Enemy>();
            spawnCooldown = initialWait;
        }

        public override void _EnterTree()
        {
            Enemy.OnDie += HandleEnemyOnDie;
            Enemy.OnNeedsToResetToSpawn += HandleResetToSpawn;
        }


        public override void _ExitTree()
        {
            Enemy.OnDie -= HandleEnemyOnDie;
            Enemy.OnNeedsToResetToSpawn -= HandleResetToSpawn;
        }

        private void HandleEnemyOnDie(Enemy which)
        {
            enemies.Remove(which);
        }

        private void HandleResetToSpawn(Enemy which)
        {
            which.ResetPath();
        }

        public override void _Process(float delta)
        {
            switch (spawnerState)
            {
                case MobSpawnerState.NotStarted:
                case MobSpawnerState.Done:
                    return;
                case MobSpawnerState.Initial:
                    ProcessInitialState(delta);
                    break;
                case MobSpawnerState.Idle:
                    ProcessIdleState();
                    break;
                case MobSpawnerState.Spawning:
                    ProcessSpawningState(delta);
                    break;
                case MobSpawnerState.Waiting:
                    ProcessWaitingState();
                    break;
                case MobSpawnerState.Cooldown:
                    ProcessCooldownState(delta);
                    break;
            }
        }

        private void ProcessCooldownState(float delta)
        {
            OnEnemyControllerEvent?.Invoke(
                new EnemyControlledTimedEvent(this, EnemyControllerEventType.CooldownStarted, waveCooldown)
            );

            waveCooldownElapsed += delta;
            if (waveCooldownElapsed >= waveCooldown)
            {
                waveCooldownElapsed = 0.0f;
                spawnerState = MobSpawnerState.Idle;
                currentWave++;
            }
        }

        private void ProcessWaitingState()
        {
            if (enemies.Count == 0)
            {
                spawnerState = MobSpawnerState.Cooldown;
            }
        }

        private void ProcessSpawningState(float delta)
        {
            spawnCooldown -= delta;

            if (spawnCooldown <= 0)
            {
                SpawnWave();
                numberSpawned++;

                if (numberSpawned >= currentWaveDef.spawnCount)
                {
                    spawnerState = MobSpawnerState.Waiting;
                }
                else
                {
                    spawnCooldown = currentWaveDef.spawnInterval;
                }
            }
        }

        private void ProcessIdleState()
        {
            if (currentWave < enemyWaveDefs.Count)
            {
                currentWaveDef = enemyWaveDefs[currentWave];
                spawnerState = MobSpawnerState.Spawning;
                spawnCooldown = 0;
                numberSpawned = 0;

                float waveLength = currentWaveDef.spawnCount * currentWaveDef.spawnInterval;
                OnEnemyControllerEvent?.Invoke(
                    new EnemyControlledTimedEvent(this, EnemyControllerEventType.WaveStart, waveLength)
                );
            }
            else
            {
                spawnerState = MobSpawnerState.Done;
                spawnCooldown = 0;
                numberSpawned = 0;
                OnEnemyControllerEvent?.Invoke(
                    new EnemyControllerEvent(this, EnemyControllerEventType.AllWavesComplete)
                );
            }
        }

        private void ProcessInitialState(float delta)
        {
            waveCooldownElapsed += delta;

            OnEnemyControllerEvent?.Invoke(
                new EnemyControlledTimedEvent(this, EnemyControllerEventType.CooldownStarted, initialWait)
            );

            if (waveCooldownElapsed >= initialWait)
            {
                spawnerState = MobSpawnerState.Idle;
                waveCooldownElapsed = 0;
            }
        }

        public void SpawnWave()
        {
            Enemy e = currentWaveDef.enemyBaseScene.Instance<Enemy>();
            e.Initialize(mobMovementRoute.Points, currentWaveDef.monsterData);
            e.Position = mobMovementRoute.GetPointPosition(0);
            enemies.Add(e);

            AddChild(e);
        }

        /// <summary>
        /// Start a level's monster spawning.
        /// </summary>
        public void StartSpawningMonsters()
        {
            if (spawnerState == MobSpawnerState.NotStarted)
            {
                spawnerState = MobSpawnerState.Initial;
            }
        }
    }
}