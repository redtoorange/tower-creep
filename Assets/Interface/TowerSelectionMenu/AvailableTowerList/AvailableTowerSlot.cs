using System;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public static Action<AvailableTowerSlot> OnAvailableSlotRightClicked;
        public static Action<AvailableTowerSlot> OnAvailableSlotDoubleClicked;

        [SerializeField] private TowerData towerData;

        public TowerData GetTowerData() => towerData;

        public Sprite GetSprite()
        {
            if (towerData != null)
            {
                return towerData.towerIcon;
            }

            return null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
            {
                OnAvailableSlotDoubleClicked?.Invoke(this);
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnAvailableSlotRightClicked?.Invoke(this);
            }
        }
    }
}