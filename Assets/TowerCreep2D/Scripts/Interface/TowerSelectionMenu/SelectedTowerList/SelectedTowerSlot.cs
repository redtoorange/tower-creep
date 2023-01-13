using System;
using TowerCreep.TowerCreep2D.Scripts.Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public static Action<SelectedTowerSlot> OnSelectedSlotDataChanged;
        public static Action<SelectedTowerSlot> OnSelectedSlotDoubleClicked;

        [SerializeField] private Image towerIconDisplay;
        private TowerData towerData;
        [SerializeField] int slotNumber = 1;

        private void Start()
        {
            GetComponent<NumberedSlot>().SetNumber(slotNumber);
        }

        public void SetTowerData(TowerData towerData)
        {
            this.towerData = towerData;

            if (!ReferenceEquals(towerData, null))
            {
                towerIconDisplay.sprite = towerData.towerIcon;
                towerIconDisplay.gameObject.SetActive(true);
            }
            else
            {
                towerIconDisplay.gameObject.SetActive(false);
            }

            OnSelectedSlotDataChanged?.Invoke(this);
        }

        public int GetSlotNumber() => slotNumber;

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
                OnSelectedSlotDoubleClicked?.Invoke(this);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                SetTowerData(null);
            }
        }
    }
}