using TowerCreep.Interface.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerDragSource : DragAndDropSource
    {
        public override object GetDragAndDropData()
        {
            return new TowerSelectionPayload(null);
        }

        public override Sprite GetDragAndDropSprite()
        {
            return GetComponent<Image>().sprite;
        }
    }
}