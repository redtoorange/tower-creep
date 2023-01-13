using TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu.SelectedTowerList;
using TowerCreep.TowerCreep2D.Scripts.Towers;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu
{
    public class TowerSwapPayload
    {
        public TowerData towerData;
        public SelectedTowerSlot sourceSlot;

        public TowerSwapPayload(TowerData towerData, SelectedTowerSlot sourceSlot)
        {
            this.towerData = towerData;
            this.sourceSlot = sourceSlot;
        }
    }
}