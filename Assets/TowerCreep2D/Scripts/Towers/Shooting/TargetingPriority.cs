using System;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.Shooting
{
    [Serializable]
    public enum TargetingPriority
    {
        ClosestFirst,
        FarthestFirst,
        WeakestFirst,
        StrongestFirst,
        MostHealth,
        LeastHealth
    }
}