using System;
using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Damage;
using UnityEngine;
using UnityJSON;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.TowerLevelData
{
    [Serializable]
    public class TowerLevelDataRecord
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

    [Serializable]
    public class TowerLevelData
    {
        [JSONNode]
        private Dictionary<int, List<TowerLevelDataRecord>> levelRecords;

        public TowerLevelData(Dictionary<string, TowerLevelDataRecord> rawRecords)
        {
            levelRecords = new Dictionary<int, List<TowerLevelDataRecord>>();

            foreach (TowerLevelDataRecord record in rawRecords.Values)
            {
                if (!levelRecords.ContainsKey(record.Level))
                {
                    levelRecords.Add(record.Level, new List<TowerLevelDataRecord>());
                }

                levelRecords[record.Level].Add(record);
            }
        }

        public List<TowerLevelDataRecord> GetData(int level)
        {
            return levelRecords[level];
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