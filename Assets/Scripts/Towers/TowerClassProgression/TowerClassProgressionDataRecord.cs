using System;
using TowerCreep.Damage;
using UnityJSON;

namespace TowerCreep.Towers.TowerClassProgression
{
    [Serializable]
    public class TowerClassProgressionDataRecord
    {
        public int Level;
        public float MinDamage;
        public float MaxDamage;
        public float Speed;
        public float Range;
        public float AOE;
        public string Abilities;
        [JSONNode(key = "Primary Type")]
        public DamageType PrimaryType;
        [JSONNode(key = "Subtype")]
        public DamageSubType SubType;
    }
}