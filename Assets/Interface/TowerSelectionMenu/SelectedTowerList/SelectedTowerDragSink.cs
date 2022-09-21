using TowerCreep.Interface.DragAndDrop;
using UnityEngine;

namespace TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerDragSink : DragAndDropSink
    {
        public override bool CanDropData(object data)
        {
            return data is TowerSelectionPayload;
        }

        public override void DropData(object data)
        {
            if (data is TowerSelectionPayload tsp)
            {
                Debug.Log("Got a TSP!");
            }
            else
            {
                Debug.Log("Not compatible");
            }
        }
    }
}