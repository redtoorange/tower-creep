using System;
using TowerCreep.Damage;
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

        [SerializeField] private MonsterData.MonsterData enemyData;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer monsterSpriteRenderer;
        [SerializeField] private Defender defender;
        [SerializeField] private EnemyHealth enemyHealth;

        private float speed = 15.0f;

        private Vector2[] movementPath;
        [SerializeField] private float navigationThreshold = 0.1f;
        private int routeIndex;

        public void Initialize(Vector2[] movementPath, MonsterData.MonsterData enemyData)
        {
            this.movementPath = movementPath;

            this.enemyData = enemyData;
            speed = enemyData.mobSpeed;
            monsterSpriteRenderer.sprite = enemyData.mobSprite;

            defender.SetDamageSinks(this.enemyData.damageSinks);

            enemyHealth.Initialize(this.enemyData.mobHealth, this.enemyData.experienceValue);
            enemyHealth.OnDie += Die;
        }

        public virtual void Die()
        {
            OnDie?.Invoke(this);
            monsterSpriteRenderer.enabled = false;
            Destroy(gameObject);
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

        public void TeleportToSpawn()
        {
            OnNeedsToResetToSpawn?.Invoke(this);
        }

        public void ResetPath()
        {
            routeIndex = 0;
            transform.position = movementPath[routeIndex];
        }

        public EnemyHealth GetHealthComponent() => enemyHealth;
    }
}