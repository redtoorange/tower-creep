﻿using System;
using TowerCreep.Enemy.Resources.MonsterData;
using TowerCreep.Map.Portals;
using UnityEngine;

namespace TowerCreep.Enemy
{
    public enum EnemyState
    {
        Initial,
        Moving,
        Idle,
        Dead
    }

    public class Enemy : MonoBehaviour
    {
        public static Action<Enemy> OnDie;
        public static Action<Enemy> OnNeedsNewPath;
        public static Action<Enemy> OnNeedsToResetToSpawn;

        [SerializeField] private MonsterData enemyData;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer monsterSpriteRenderer;

        private float maxHealth = 10.0f;
        private float health = 10.0f;
        private float speed = 15.0f;

        private Vector2[] movementPath;
        [SerializeField] private float navigationThreshold = 0.1f;
        private int routeIndex;

        public void Initialize(Vector2[] movementPath, MonsterData enemyData)
        {
            this.movementPath = movementPath;

            this.enemyData = enemyData;
            maxHealth = enemyData.mobHealth;
            health = maxHealth;
            speed = enemyData.mobSpeed;
            monsterSpriteRenderer.sprite = enemyData.mobSprite;
        }

        public virtual void Die()
        {
            OnDie?.Invoke(this);
            monsterSpriteRenderer.enabled = false;
            Destroy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            // healthBar.Value = 100.0f * (health / maxHealth);
            // if (healthBar.Value < 100.0f)
            // {
            //     healthBar.Visible = true;
            // }

            if (health <= 0)
            {
                Die();
            }
        }


        private void FixedUpdate()
        {
            if (routeIndex < movementPath.Length)
            {
                // Handle movement using the Physics engine
                Vector2 targetPoint = movementPath[routeIndex];
                float dist = Vector3.Distance(targetPoint, transform.position);
                if (dist > navigationThreshold)
                {
                    Vector2 direction = targetPoint - (Vector2)transform.position;
                    rigidbody2D.MovePosition(rigidbody2D.position +
                                             (direction.normalized * (speed * Time.fixedDeltaTime)));
                }
                else
                {
                    routeIndex++;
                }
            }
        }

        public void TeleportToSpawn(Portal spawnLocation)
        {
            OnNeedsToResetToSpawn?.Invoke(this);
        }

        public void ResetPath()
        {
            routeIndex = 0;
            transform.position = movementPath[routeIndex];
        }
    }
}