using System;
using TowerCreep.Player.TowerCollection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerCreep.Interface.HotBar
{
    public class TowerHotBarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Bubble up events
        public Action<TowerHotBarSlot> OnButtonSlotPressed;

        [SerializeField] private Sprite numberSprite;
        public TowerCollectionSlot CollectionSlot
        {
            get => _collectionSlot;
            set => SetCollectionData(value);
        }
        private TowerCollectionSlot _collectionSlot;

        // Internal Nodes
        [SerializeField] private Image selectedBorderImage;
        [SerializeField] private Image unselectedBorderImage;
        [SerializeField] private Image numberImage;
        [SerializeField] private Image towerDisplayImage;

        // Internal State
        private bool isSelected = false;
        private bool isHovered = false;
        public bool IsAvailable { get; private set; }

        private void Start()
        {
            UpdateLabel();
            UpdateSelected();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
            SetSelected(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
            SetSelected(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonSlotPressed?.Invoke(this);
        }

        // public override void _Input(InputEvent @event)
        // {
        //     if (!isHovered || !IsAvailable) return;
        //
        //     if (@event is InputEventMouseButton mb)
        //     {
        //         if (mb.Pressed && mb.ButtonIndex == (int)ButtonList.Left)
        //         {
        //             OnButtonSlotPressed?.Invoke(this);
        //         }
        //     }
        // }

        private void UpdateLabel()
        {
            if (numberImage == null) return;

            numberImage.sprite = numberSprite;
        }

        private void SetCollectionData(TowerCollectionSlot collectionSlot)
        {
            _collectionSlot = collectionSlot;
            // HintTooltip = _collectionSlot.CollectionTowerData.towerName;
            SetAvailable(true);
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
                towerDisplayImage.sprite = _collectionSlot.CollectionTowerData.disabledTowerIcon;
            }
            else
            {
                towerDisplayImage.sprite = _collectionSlot.CollectionTowerData.towerIcon;
            }
        }
    }
}