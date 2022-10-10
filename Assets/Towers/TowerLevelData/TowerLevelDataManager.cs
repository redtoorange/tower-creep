using TowerCreep.Towers;
using UnityEngine;
using UnityJSON;

namespace TowerCreep
{
    public class TowerLevelDataManager : MonoBehaviour
    {
        public static TowerLevelDataManager S;

        private TowerLevelData archerData;
        private TowerLevelData arcaneData;
        private TowerLevelData daggerData;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
                DontDestroyOnLoad(gameObject);

                archerData = TowerDataParser.LoadTowerLevelData("Archer");
                arcaneData = TowerDataParser.LoadTowerLevelData("Arcane");
                daggerData = TowerDataParser.LoadTowerLevelData("Dagger");

                Debug.Log(JSON.Serialize(archerData.GetData(1)));
            }
            else
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }

        public TowerLevelData GetLevelData(TowerData towerData)
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