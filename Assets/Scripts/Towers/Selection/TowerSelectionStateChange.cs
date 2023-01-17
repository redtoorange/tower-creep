using System;

namespace TowerCreep.Towers.Selection
{
    [Serializable]
    public class TowerSelectionStateChange
    {
        public Tower tower;
        public TowerHoverState oldHoveredState;
        public TowerHoverState newHoveredState;
        public TowerSelectionState oldSelectedState;
        public TowerSelectionState newSelectedState;

        public TowerSelectionStateChange(Tower tower, TowerHoverState oldHoveredState, TowerHoverState newHoveredState,
            TowerSelectionState oldSelectedState, TowerSelectionState newSelectedState)
        {
            this.tower = tower;
            this.oldHoveredState = oldHoveredState;
            this.newHoveredState = newHoveredState;
            this.oldSelectedState = oldSelectedState;
            this.newSelectedState = newSelectedState;
        }
    }
}