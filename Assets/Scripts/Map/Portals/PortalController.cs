using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.Map.Portals
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] private List<SpawnPortal> spawnPortals;
        [SerializeField] private List<ExitPortal> exitPortals;

        public List<SpawnPortal> GetSpawnPortals() => spawnPortals;
        public List<ExitPortal> GetExitPortals() => exitPortals;
    }
}