using System;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Map.Portals
{
    [Serializable]
    public enum PortalType
    {
        Entrance,
        Exit
    }

    public class Portal : MonoBehaviour
    {
        public static Action OnEnemyReachedExit;

        [SerializeField] private PortalType portalType = PortalType.Entrance;

        private Portal spawnLocation;

        public void ExitInitialize(Portal spawnLocation)
        {
            this.spawnLocation = spawnLocation;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (portalType != PortalType.Exit) return;

            if (other.TryGetComponent(out Enemy.Enemy e))
            {
                OnEnemyReachedExit?.Invoke();
                e.TeleportToSpawn(spawnLocation);
            }
        }
    }
}