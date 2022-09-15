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
        [SerializeField] private Rigidbody2D rigidBody;

        // Internal State
        private bool hasTarget;
        private Vector2 targetPoint;
        private Vector2 targetDirection;

        public void FireAt(Enemy.Enemy enemy)
        {
            if (enemy != null)
            {
                Vector2 position = transform.position;
                targetPoint = enemy.transform.position;
                targetDirection = (targetPoint - position).normalized;
                
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90.0f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                rigidBody.velocity = speed * targetDirection;

                hasTarget = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out Enemy.Enemy e))
            {
                e.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                rigidBody.velocity = Vector2.zero;
                rigidBody.simulated = false;
                rigidBody.Sleep();
            }
        }
    }
}