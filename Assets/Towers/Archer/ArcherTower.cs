using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Towers.Shooting;

namespace TowerCreep.Towers.Archer
{
    public class ArcherTower : Tower
    {
        // External Nodes
        private TowerRangeDetection towerRangeDetection;

        // Exported Properties
        [Export] private float shootingDelay = 0.5f;
        [Export] private PackedScene arrowStatueProjectileScene;

        // Internal State
        private List<Enemy.Enemy> allEnemiesInRange;
        private Enemy.Enemy currentEnemy;
        private float currentCooldown = 0.0f;

        private AudioStreamPlayer2D soundPlayer;

        public override void _Ready()
        {
            soundPlayer = GetNode<AudioStreamPlayer2D>("TowerSoundPlayer");
            allEnemiesInRange = new List<Enemy.Enemy>();
        }

        public override void _EnterTree()
        {
            towerRangeDetection = GetNode<TowerRangeDetection>("TowerRangeDetection");
            towerRangeDetection.OnEnemyHasEnteredRange += AddEnemy;
            towerRangeDetection.OnEnemyHasExitedRange += RemoveEnemy;

            Enemy.Enemy.OnDie += HandleEnemyDie;
        }


        public override void _ExitTree()
        {
            towerRangeDetection.OnEnemyHasEnteredRange -= AddEnemy;
            towerRangeDetection.OnEnemyHasExitedRange -= RemoveEnemy;

            Enemy.Enemy.OnDie -= HandleEnemyDie;
        }

        public override void _Process(float delta)
        {
            // Cooldown ticks down to 0 or less
            if (currentCooldown > 0)
            {
                currentCooldown -= delta;
                return;
            }

            if (currentEnemy != null)
            {
                FireShot();
                currentCooldown = shootingDelay;
            }
        }

        private void FireShot()
        {
            Projectile projectile = arrowStatueProjectileScene.Instance<Projectile>();
            AddChild(projectile);
            projectile.FireAt(currentEnemy);
            soundPlayer.Play();
        }

        private void HandleEnemyDie(Enemy.Enemy enemy)
        {
            if (currentEnemy == enemy)
            {
                RemoveEnemy(enemy);
            }
        }

        private void AddEnemy(Enemy.Enemy enemy)
        {
            allEnemiesInRange.Add(enemy);
            if (currentEnemy == null)
            {
                FindClosestEnemy();
            }
        }

        private void RemoveEnemy(Enemy.Enemy enemy)
        {
            if (allEnemiesInRange.Contains(enemy))
            {
                allEnemiesInRange.Remove(enemy);
            }

            if (enemy == currentEnemy)
            {
                currentEnemy = null;
                FindClosestEnemy();
            }
        }

        private void FindClosestEnemy()
        {
            if (allEnemiesInRange.Count > 0)
            {
                currentEnemy = allEnemiesInRange[0];
                float closestDistance = Position.DistanceSquaredTo(currentEnemy.Position);
                for (int i = 1; i < allEnemiesInRange.Count; i++)
                {
                    float tempDist = Position.DistanceSquaredTo(allEnemiesInRange[i].Position);
                    if (tempDist < closestDistance)
                    {
                        closestDistance = tempDist;
                        currentEnemy = allEnemiesInRange[i];
                    }
                }
            }
        }
    }
}