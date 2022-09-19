using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionPayload
    {
        public TowerData towerData;

        public TowerSelectionPayload(TowerData towerData)
        {
            this.towerData = towerData;
        }
    }
}