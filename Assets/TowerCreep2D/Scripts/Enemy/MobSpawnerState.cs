using System;

namespace TowerCreep.TowerCreep2D.Scripts.Enemy
{
    [Serializable]
    public enum MobSpawnerState
    {
        NotStarted,
        Idle,
        Spawning,
        Waiting,
        Cooldown,
        Done
    }
}