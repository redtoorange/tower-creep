using System;

namespace TowerCreep.Damage
{
    [Serializable]
    public struct DamageSink
    {
        public DamageType defenseType;
        public DamageSubType defenseSubType;
        public float defensePercent;
    }
}