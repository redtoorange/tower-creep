using UnityEngine;

namespace TowerCreep.Towers.TowerClassProgression
{
    public class TowerClassProgressionDataManager : MonoBehaviour
    {
        public static TowerClassProgressionDataManager S;

        private TowerClassProgressionData archerData;
        private TowerClassProgressionData arcaneData;
        private TowerClassProgressionData daggerData;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
                DontDestroyOnLoad(gameObject);

                archerData = TowerClassProgressionDataParser.LoadTowerLevelData("Archer");
                arcaneData = TowerClassProgressionDataParser.LoadTowerLevelData("Arcane");
                daggerData = TowerClassProgressionDataParser.LoadTowerLevelData("Dagger");
            }
            else
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }

        public TowerClassProgressionData GetLevelData(TowerData towerData)
        {
            switch (towerData.towerName)
            {
                case "Archer":
                    return archerData;
                case "Arcane":
                    return arcaneData;
                case "Dagger":
                    return daggerData;
                default:
                    return null;
            }
        }
    }
}