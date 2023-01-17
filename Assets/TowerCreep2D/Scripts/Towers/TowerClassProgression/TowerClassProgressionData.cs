using System;
using System.Collections.Generic;
using UnityJSON;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.TowerClassProgression
{
    [Serializable]
    public class TowerClassProgressionData
    {
        [JSONNode]
        private Dictionary<int, List<TowerClassProgressionDataRecord>> levelRecords;

        public TowerClassProgressionData(Dictionary<string, TowerClassProgressionDataRecord> rawRecords)
        {
            levelRecords = new Dictionary<int, List<TowerClassProgressionDataRecord>>();

            foreach (TowerClassProgressionDataRecord record in rawRecords.Values)
            {
                if (!levelRecords.ContainsKey(record.Level))
                {
                    levelRecords.Add(record.Level, new List<TowerClassProgressionDataRecord>());
                }

                levelRecords[record.Level].Add(record);
            }
        }

        public List<TowerClassProgressionDataRecord> GetData(int level)
        {
            return levelRecords[level];
        }
    }
}