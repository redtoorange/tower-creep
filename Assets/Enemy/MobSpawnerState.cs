using System;

namespace TowerCreep.Enemy
{
    [Serializable]
    public enum MobSpawnerState
    {
        NotStarted,
        Initial,
        Idle,
        Spawning,
        Waiting,
        Cooldown,
        Done
    }
}