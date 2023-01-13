using TowerCreep.TowerCreep2D.Scripts.Interface.Tooltip;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedSlotToolTipTarget : ToolTipTarget
    {
        private SelectedTowerSlot towerSlot;

        private void Start()
        {
            towerSlot = GetComponent<SelectedTowerSlot>();
        }

        public override bool ShouldDisplayToolTip()
        {
            return !ReferenceEquals(towerSlot.GetTowerData(), null);
        }

        public override string GetText()
        {
            return towerSlot.GetTowerData().towerName;
        }
    }
}