using TowerCreep.TowerCreep2D.Scripts.Towers;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu
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