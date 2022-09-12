using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public abstract class HoverableSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected bool isHovered = false;

        // public override void _Input(InputEvent @event)
        // {
        //     if (!isHovered) return;
        //
        //     if (@event is InputEventMouseButton inputEventMouseButton)
        //     {
        //         if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Left && inputEventMouseButton.Doubleclick)
        //         {
        //             HandleDoubleLeftClicked();
        //         }
        //         else if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Right && inputEventMouseButton.Pressed)
        //         {
        //             HandleRightClicked();
        //         }
        //     }
        // }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
        }

        protected abstract void HandleDoubleLeftClicked();
        protected abstract void HandleRightClicked();
    }
}