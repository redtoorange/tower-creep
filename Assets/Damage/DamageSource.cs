using System;

namespace TowerCreep.Damage
{
    [Serializable]
    public struct DamageSource
    {
        public DamageType damageType;
        public DamageSubType damageSubType;
        public int damageMinAmount;
        public int damageMaxAmount;
    }
}