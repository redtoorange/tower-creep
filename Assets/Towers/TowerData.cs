using UnityEngine;

namespace TowerCreep.Towers
{
    [CreateAssetMenu(fileName = "Data", menuName = "TowerCreep/TowerData", order = 1)]
    public class TowerData : ScriptableObject
    {
        public Sprite towerIcon;
        public Sprite disabledTowerIcon;
        public string towerName = "Test Data";
        public Tower towerPrefab;
        public string towerInformation = "";
    }
}