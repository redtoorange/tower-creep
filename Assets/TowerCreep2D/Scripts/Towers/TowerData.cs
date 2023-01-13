using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Towers
{
    [CreateAssetMenu(fileName = "Data", menuName = "TowerCreep/TowerData", order = 1)]
    public class TowerData : ScriptableObject
    {
        public Sprite towerIcon;
        public Sprite disabledTowerIcon;
        public string towerName = "Test Data";
        public Tower towerPrefab;
        [TextArea(15, 20)]
        public string towerInformation = "";
    }
}