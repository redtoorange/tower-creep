using System;

namespace TowerCreep.Enemy
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