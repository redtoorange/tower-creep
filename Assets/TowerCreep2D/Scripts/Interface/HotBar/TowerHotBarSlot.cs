using System;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.HotBar
{
    public class TowerHotBarSlot : MonoBehaviour, IPointerClickHandler
    {
        // Bubble up events
        public Action<TowerHotBarSlot> OnButtonSlotPressed;

        [SerializeField] private Sprite numberSprite;
        private PlayerPartySlot collectionSlot;

        // Internal Nodes
        [SerializeField] private Image selectedBorderImage;
        [SerializeField] private Image unselectedBorderImage;
        [SerializeField] private Image numberImage;
        [SerializeField] private Image towerDisplayImage;

        // Internal State
        private bool isSelected = false;
        public bool IsAvailable { get; private set; }

        private void Start()
        {
            UpdateLabel();
            UpdateSelected();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonSlotPressed?.Invoke(this);
        }

        private void UpdateLabel()
        {
            if (numberImage == null) return;

            numberImage.sprite = numberSprite;
        }

        public PlayerPartySlot GetCollectionSlotData() => collectionSlot;

        public void SetCollectionSlotData(PlayerPartySlot collectionSlot)
        {
            this.collectionSlot = collectionSlot;

            if (!ReferenceEquals(collectionSlot, null) && !ReferenceEquals(collectionSlot.CollectionTowerData, null))
            {
                towerDisplayImage.gameObject.SetActive(true);
                SetAvailable(true);
            }
            else
            {
                towerDisplayImage.gameObject.SetActive(false);
            }
        }

        public void SetSelected(bool isSelected)
        {
            if (isSelected != this.isSelected)
            {
                this.isSelected = isSelected;
                UpdateSelected();
            }
        }

        private void UpdateSelected()
        {
            if (selectedBorderImage == null || unselectedBorderImage == null) return;

            selectedBorderImage.gameObject.SetActive(isSelected);
            unselectedBorderImage.gameObject.SetActive(!isSelected);
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public void SetAvailable(bool available)
        {
            IsAvailable = available;
            if (!IsAvailable)
            {
                towerDisplayImage.sprite = collectionSlot.CollectionTowerData.disabledTowerIcon;
            }
            else
            {
                towerDisplayImage.sprite = collectionSlot.CollectionTowerData.towerIcon;
            }
        }
    }
}