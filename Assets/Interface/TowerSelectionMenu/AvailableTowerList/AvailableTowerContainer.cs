using UnityEngine;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerContainer : MonoBehaviour
    {
        public bool CanDropData(Vector2 position, object data)
        {
            return data is TowerSwapPayload;
        }

        public void DropData(Vector2 position, object data)
        {
            if (data is TowerSwapPayload tsp)
            {
                tsp.sourceSlot.ClearTowerData();
            }
        }
    }
}