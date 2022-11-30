using System;

namespace TowerCreep.Towers.Shooting
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