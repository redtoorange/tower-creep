using System;
using Godot;
using TowerCreep.Enemy.Resources.MonsterData;
using Portal = TowerCreep.Map.Portals.Portal;

namespace TowerCreep.Enemy
{
    public enum EnemyState
    {
        Initial,
        Moving,
        Idle,
        Dead
    }

    public class Enemy : KinematicBody2D
    {
        public static Action<Enemy> OnDie;
        public static Action<Enemy> OnNeedsNewPath;
        public static Action<Enemy> OnNeedsToResetToSpawn;

        private float maxHealth = 10.0f;
        private float health = 10.0f;
        private float speed = 15.0f;

        private float navigationThreshold = 1.5f;
        private Vector2[] movementPath;
        private MonsterData enemyData;
        private int routeIndex;
        private ProgressBar healthBar;
        private Sprite monsterSprite;


        public override void _Ready()
        {
            base._Ready();

            healthBar = GetNode<ProgressBar>("HealthBar/ProgressBar");
            monsterSprite = GetNode<Sprite>("Sprite");
            monsterSprite.Texture = enemyData.mobSprite;
            healthBar.Visible = false;
        }

        public void Initialize(Vector2[] movementPath, MonsterData enemyData)
        {
            this.movementPath = movementPath;

            this.enemyData = enemyData;
            maxHealth = enemyData.mobHealth;
            health = maxHealth;
            speed = enemyData.mobSpeed;
        }

        public virtual void Die()
        {
            OnDie?.Invoke(this);
            Visible = false;
            QueueFree();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.Value = 100.0f * (health / maxHealth);
            if (healthBar.Value < 100.0f)
            {
                healthBar.Visible = true;
            }

            if (health <= 0)
            {
                Die();
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            if (routeIndex < movementPath.Length)
            {
                // Handle movement using the Physics engine
                Vector2 targetPoint = movementPath[routeIndex];
                float dist = Position.DistanceTo(targetPoint);
                if (dist > navigationThreshold)
                {
                    Vector2 direction = targetPoint - Position;
                    MoveAndCollide(direction.Normalized() * speed * delta);
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
            Position = movementPath[routeIndex];
        }
    }
}