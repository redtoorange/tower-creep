using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.TowerLevelData
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