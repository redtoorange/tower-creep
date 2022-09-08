using UnityEngine;

namespace TowerCreep.Towers.Shooting
{
    public class Projectile : MonoBehaviour
    {
        // SerializeFielded Properties
        [SerializeField] private float speed = 100.0f;
        [SerializeField] private float damage = 1.0f;
        [SerializeField] private bool keepOnMap = true;

        // External Nodes
        private Rigidbody2D rigidBody;

        // Internal State
        private bool hasTarget;
        private Vector2 targetPoint;
        private Vector2 targetDirection;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public void FireAt(Enemy.Enemy enemy)
        {
            if (enemy != null)
            {
                Vector2 position = transform.position;
                targetPoint = enemy.transform.position;
                targetDirection = (targetPoint - position).normalized;
                transform.rotation = Quaternion.LookRotation(targetDirection);

                hasTarget = true;
            }
        }

        private void FixedUpdate()
        {
            if (!hasTarget) return;

            // MoveAndSlide(targetDirection * speed);
            // int slideCount = GetSlideCount();
            // for (int i = 0; i < slideCount; i++)
            // {
            //     KinematicCollision2D collision = GetSlideCollision(i);
            //     if (collision != null)
            //     {
            //         bool playSound = false;
            //         if (collision.Collider is Enemy.Enemy e)
            //         {
            //             playSound = true;
            //             e.TakeDamage(damage);
            //             hasTarget = false;
            //             rigidBody.Disabled = true;
            //             Visible = false;
            //             keepOnMap = false;
            //         }
            //         else if (collision.Collider is TileMap sb || collision.Collider is Door d)
            //         {
            //             playSound = true;
            //             hasTarget = false;
            //             rigidBody.Disabled = true;
            //
            //             if (!keepOnMap)
            //             {
            //                 Visible = false;
            //             }
            //         }
            //
            //         if (playSound)
            //         {
            //             // Play sound
            //         }
            //     }
            // }
        }
    }
}