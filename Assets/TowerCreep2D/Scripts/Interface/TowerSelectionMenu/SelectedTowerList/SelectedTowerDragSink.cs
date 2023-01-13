using TowerCreep.TowerCreep2D.Scripts.Interface.DragAndDrop;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerDragSink : DragAndDropSink
    {
        private SelectedTowerSlot towerSlot;

        private void Start()
        {
            towerSlot = GetComponent<SelectedTowerSlot>();
        }

        public override bool CanDropData(object data)
        {
            return data is TowerSelectionPayload || data is TowerSwapPayload;
        }

        public override void DropData(object data)
        {
            if (data is TowerSelectionPayload towerSelect)
            {
                towerSlot.SetTowerData(towerSelect.towerData);
            }
            else if (data is TowerSwapPayload towerSwap)
            {
                towerSwap.sourceSlot.SetTowerData(towerSlot.GetTowerData());
                towerSlot.SetTowerData(towerSwap.towerData);
            }
            else
            {
                Debug.Log("Not compatible");
            }
        }
    }
}