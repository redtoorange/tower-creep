using System;
using UnityEngine;

namespace TowerCreep.Towers.Shooting
{
    public class TowerRangeDetection : MonoBehaviour
    {
        public Action<Enemy.Enemy> OnEnemyHasEnteredRange;
        public Action<Enemy.Enemy> OnEnemyHasExitedRange;

        private CircleCollider2D circleCollider;

        private void Awake()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        public void SetRange(float range)
        {
            circleCollider.radius = range;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy e))
            {
                OnEnemyHasEnteredRange?.Invoke(e);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy e))
            {
                OnEnemyHasExitedRange?.Invoke(e);
            }
        }
    }
}