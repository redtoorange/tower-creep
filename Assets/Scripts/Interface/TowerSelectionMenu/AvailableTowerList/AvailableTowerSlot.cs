using System;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public static Action<AvailableTowerSlot> OnAvailableSlotRightClicked;
        public static Action<AvailableTowerSlot> OnAvailableSlotDoubleClicked;

        [SerializeField] private TowerData towerData;
        [SerializeField] private bool available = false;

        private Image towerImage;

        private void Start()
        {
            towerImage = GetComponent<Image>();
            if (towerData != null)
            {
                if (available)
                {
                    towerImage.sprite = towerData.towerIcon;
                }
                else
                {
                    towerImage.sprite = towerData.disabledTowerIcon;
                }
            }
        }

        public TowerData GetTowerData() => towerData;

        public Sprite GetSprite()
        {
            if (!ReferenceEquals(towerData, null))
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
            if (!available) return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnAvailableSlotRightClicked?.Invoke(this);
            }
        }

        public bool IsAvailable()
        {
            return towerData != null && available;
        }
    }
}