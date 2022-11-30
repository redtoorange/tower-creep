using System;
using System.Collections.Generic;
using TowerCreep.Enemy.EnemyControllerEvents;
using TowerCreep.Enemy.WaveData;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers.Placement;
using UnityEngine;

namespace TowerCreep.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public static Action<EnemyControllerEvent> OnEnemyControllerEvent;

        [SerializeField] private float initialWait = 10.0f;
        [SerializeField] private List<EnemyWaveData> enemyWaveDefs;

        // Spawning Logic
        private EnemyWaveData currentWaveDef;
        private float spawnCooldown;
        private int numberSpawned;

        [SerializeField]
        private MobSpawnerState spawnerState = MobSpawnerState.NotStarted;
        private int currentWave = 0;
        [SerializeField] private float waveCooldown = 10.0f;
        private float waveCooldownElapsed = 0.0f;

        // Enemy Pathing
        [SerializeField] private LineRenderer mobMovementRoutePath;

        // Enemy Tracking
        private List<Enemy> enemies;
        private Vector2[] routePosition;

        // Tower Interactions
        private TowerController towerController;
        
        private void Start()
        {
            enemies = new List<Enemy>();
            spawnCooldown = initialWait;
            mobMovementRoutePath.enabled = false;

            routePosition = new Vector2[mobMovementRoutePath.positionCount];
            for (int i = 0; i < mobMovementRoutePath.positionCount; i++)
            {
                routePosition[i] = transform.TransformPoint(mobMovementRoutePath.GetPosition(i));
            }

            towerController = FindObjectOfType<TowerController>();
        }

        private void OnEnable()
        {
            Enemy.OnDie += HandleEnemyOnDie;
            Enemy.OnNeedsToResetToSpawn += HandleResetToSpawn;
        }


        private void OnDisable()
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

        private void Update()
        {
            switch (spawnerState)
            {
                case MobSpawnerState.NotStarted:
                case MobSpawnerState.Done:
                    return;
                case MobSpawnerState.Idle:
                    ProcessIdleState();
                    break;
                case MobSpawnerState.Spawning:
                    ProcessSpawningState(Time.deltaTime);
                    break;
                case MobSpawnerState.Waiting:
                    ProcessWaitingState();
                    break;
                case MobSpawnerState.Cooldown:
                    ProcessCooldownState(Time.deltaTime);
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
                
                // Reward all towers
                if (currentWaveDef.experienceCompletionValue > 0)
                {
                    towerController.GiveExperienceToAll(currentWaveDef.experienceCompletionValue);
                }
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

        public void SpawnWave()
        {
            Enemy e = Instantiate(currentWaveDef.enemyBasePrefab, transform);

            if (!ReferenceEquals(mobMovementRoutePath, null) && mobMovementRoutePath.positionCount > 0)
            {
                e.Initialize(routePosition, currentWaveDef.monsterData);
                e.transform.position = routePosition[0];
            }

            enemies.Add(e);
        }

        /// <summary>
        /// Start a level's monster spawning.
        /// </summary>
        public void StartSpawningMonsters()
        {
            if (spawnerState == MobSpawnerState.NotStarted)
            {
                spawnerState = MobSpawnerState.Idle;
            }
        }
    }
}