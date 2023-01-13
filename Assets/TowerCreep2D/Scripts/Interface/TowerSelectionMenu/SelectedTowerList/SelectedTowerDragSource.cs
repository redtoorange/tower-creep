using TowerCreep.TowerCreep2D.Scripts.Interface.DragAndDrop;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerDragSource : DragAndDropSource
    {
        private SelectedTowerSlot towerSlot;

        private void Start()
        {
            towerSlot = GetComponent<SelectedTowerSlot>();
        }

        public override object GetDragAndDropData()
        {
            return new TowerSwapPayload(
                towerSlot.GetTowerData(),
                towerSlot
            );
        }

        public override Sprite GetDragAndDropSprite()
        {
            return towerSlot.GetSprite();
        }

        public override bool CanStartDragging()
        {
            return towerSlot.GetTowerData() != null;
        }
    }
}