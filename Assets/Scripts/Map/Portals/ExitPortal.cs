using System;
using UnityEngine;

namespace TowerCreep.Map.Portals
{
    public class ExitPortal : MonoBehaviour
    {
        public static Action OnEnemyReachedExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy.Enemy e))
            {
                OnEnemyReachedExit?.Invoke();
                e.TeleportToSpawn();
            }
        }
    }
}