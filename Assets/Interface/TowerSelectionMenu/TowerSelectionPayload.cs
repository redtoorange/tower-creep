using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionPayload : Object
    {
        public TowerData towerData;

        public TowerSelectionPayload(TowerData towerData)
        {
            this.towerData = towerData;
        }
    }
}