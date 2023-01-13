using System;
using TowerCreep.TowerCreep2D.Scripts.Towers.TowerLevelData;

namespace TowerCreep.TowerCreep2D.Scripts.Damage
{
    [Serializable]
    public struct DamageSource
    {
        public DamageType damageType;
        public DamageSubType damageSubType;
        public float damageMinAmount;
        public float damageMaxAmount;

        public static DamageSource FromData(TowerLevelDataRecord record)
        {
            return new DamageSource()
            {
                damageType = record.PrimaryType,
                damageSubType = record.SubType,
                damageMinAmount = record.MinDamage,
                damageMaxAmount = record.MaxDamage
            };
        }
    }
}