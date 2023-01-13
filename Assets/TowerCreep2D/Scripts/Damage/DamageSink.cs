using System;

namespace TowerCreep.TowerCreep2D.Scripts.Damage
{
    [Serializable]
    public struct DamageSink
    {
        public DamageType defenseType;
        public DamageSubType defenseSubType;
        public float defensePercent;
    }
}