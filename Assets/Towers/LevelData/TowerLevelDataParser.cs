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
        [JSONNode]
        private Dictionary<string, TowerLevelDataRecord> records;

        public TowerLevelData(Dictionary<string, TowerLevelDataRecord> records)
        {
            this.records = records;
        }

        public TowerLevelDataRecord GetData(int level)
        {
            return records[level.ToString()];
        }
    }


    public class TowerDataParser
    {
        public static TowerLevelData LoadTowerLevelData(string name)
        {
            TextAsset asset = Resources.Load<TextAsset>($"Text/{name}");
            if (ReferenceEquals(asset, null))
            {
                return null;
            }

            return new TowerLevelData(
                JSON.Deserialize<Dictionary<string, TowerLevelDataRecord>>(asset.text)
            );
        }
    }
}