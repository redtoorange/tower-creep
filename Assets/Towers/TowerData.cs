using UnityEngine;

namespace TowerCreep.Towers
{
    [CreateAssetMenu(fileName = "Data", menuName = "TowerCreep/TowerData", order = 1)]
    public class TowerData : ScriptableObject
    {
        public Texture towerIcon;
        public Texture disabledTowerIcon;
        public string towerName = "Test Data";
        public int towerBaseCost = 100;
        public Tower towerPrefab;
        public string towerInformation = "";
    }
}