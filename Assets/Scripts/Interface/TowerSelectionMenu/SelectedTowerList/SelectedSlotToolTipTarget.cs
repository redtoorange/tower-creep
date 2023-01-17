using TowerCreep.Interface.Tooltip;

namespace TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList
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