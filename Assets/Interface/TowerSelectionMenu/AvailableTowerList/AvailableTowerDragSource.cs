using TowerCreep.Interface.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerDragSource : DragAndDropSource
    {
        private AvailableTowerSlot towerSlot;

        private void Start()
        {
            towerSlot = GetComponent<AvailableTowerSlot>();
        }
        
        public override object GetDragAndDropData()
        {
            return new TowerSelectionPayload(towerSlot.GetTowerData());
        }

        public override Sprite GetDragAndDropSprite()
        {
            return towerSlot.GetSprite();
        }
    }
}