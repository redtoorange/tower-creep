using System.Collections.Generic;
using UnityEngine;
using UnityJSON;

namespace TowerCreep.Towers.TowerClassProgression
{
    public class TowerClassProgressionDataParser
    {
        public static TowerClassProgressionData LoadTowerLevelData(string name)
        {
            TextAsset asset = Resources.Load<TextAsset>($"Text/{name}");
            if (ReferenceEquals(asset, null))
            {
                return null;
            }

            return new TowerClassProgressionData(
                JSON.Deserialize<Dictionary<string, TowerClassProgressionDataRecord>>(asset.text)
            );
        }
    }
}