using System;
using TowerCreep.TowerCreep2D.Scripts.Damage;
using UnityJSON;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.TowerClassProgression
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