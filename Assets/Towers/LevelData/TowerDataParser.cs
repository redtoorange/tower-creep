using System;
using System.Collections.Generic;
using UnityEngine;
using UnityJSON;

namespace TowerCreep
{
    [Serializable]
    public class TowerLevelDataRecord
    {
        public float Damage;
        public float Speed;
        public float Range;
        public float AOE;
        public string Abilities;
    }

    [Serializable]
    public class TowerLevelData
    {
        public Dictionary<string, TowerLevelDataRecord> Archer;
        public Dictionary<string, TowerLevelDataRecord> Dagger;
        public Dictionary<string, TowerLevelDataRecord> Arcane;
    }


    public class TowerDataParser
    {
        public static TowerLevelData LoadTowerLevelData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Text/Tower Creep");
            return JSON.Deserialize<TowerLevelData>(jsonFile.text);
        }
    }
}