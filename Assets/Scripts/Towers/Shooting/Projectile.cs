using TowerCreep.Damage;
using UnityEngine;

namespace TowerCreep.Towers.Shooting
{
    public class Projectile : MonoBehaviour
    {
        // SerializeFielded Properties
        [SerializeField] private float speed = 100.0f;
        [SerializeField] private bool keepOnMap = true;

        // External Nodes
        [SerializeField] private Rigidbody2D rigidBody;

        // Internal State
        private Vector2 targetPoint;
        private Vector2 targetDirection;
        private Attack attack;

        public void FireAt(Attack attack, Enemy.Enemy enemy)
        {
            this.attack = attack;
            if (!ReferenceEquals(enemy, null))
            {
                Vector2 position = transform.position;
                targetPoint = enemy.transform.position;
                targetDirection = (targetPoint - position).normalized;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90.0f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                rigidBody.velocity = speed * targetDirection;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out Enemy.Enemy e))
            {
                if (e.TryGetComponent(out Defender d))
                {
                    float damage = DamageSystem.S.ProcessAttack(attack, d);
                    e.TakeDamage(damage, attack.source);
                }
                Destroy(gameObject);
            }
            else
            {
                if (keepOnMap)
                {
                    rigidBody.velocity = Vector2.zero;
                    rigidBody.simulated = false;
                    rigidBody.Sleep();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}