using System.Collections.Generic;
using TowerCreep.Towers.Shooting;
using UnityEngine;

namespace TowerCreep.Towers.Archer
{
    public class ArcherTower : Tower
    {
        // External Nodes
        private TowerRangeDetection towerRangeDetection;

        // SerializeFielded Properties
        [SerializeField] private float shootingDelay = 0.5f;
        [SerializeField] private Projectile arrowStatueProjectilePrefab;

        // Internal State
        private List<Enemy.Enemy> allEnemiesInRange;
        private Enemy.Enemy currentEnemy;
        private float currentCooldown = 0.0f;


        private void Start()
        {
            allEnemiesInRange = new List<Enemy.Enemy>();
        }

        private void OnEnable()
        {
            towerRangeDetection = GetComponentInChildren<TowerRangeDetection>();
            towerRangeDetection.OnEnemyHasEnteredRange += AddEnemy;
            towerRangeDetection.OnEnemyHasExitedRange += RemoveEnemy;

            Enemy.Enemy.OnDie += HandleEnemyDie;
        }

        private void OnDisable()
        {
            towerRangeDetection.OnEnemyHasEnteredRange -= AddEnemy;
            towerRangeDetection.OnEnemyHasExitedRange -= RemoveEnemy;

            Enemy.Enemy.OnDie -= HandleEnemyDie;
        }

        private void Update()
        {
            // Cooldown ticks down to 0 or less
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
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
            Projectile projectile = Instantiate(arrowStatueProjectilePrefab);
            projectile.FireAt(currentEnemy);
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
                Vector2 position = transform.position;
                float closestDistance = Vector2.Distance(position, currentEnemy.transform.position);
                for (int i = 1; i < allEnemiesInRange.Count; i++)
                {
                    float tempDist = Vector2.Distance(position, allEnemiesInRange[i].transform.position);
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