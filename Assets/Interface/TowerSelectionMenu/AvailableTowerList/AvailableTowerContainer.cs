using Godot;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerContainer : Control
    {
        public override bool CanDropData(Vector2 position, object data)
        {
            return data is TowerSwapPayload;
        }

        public override void DropData(Vector2 position, object data)
        {
            if (data is TowerSwapPayload tsp)
            {
                tsp.sourceSlot.ClearTowerData();
            }
        }
    }
}