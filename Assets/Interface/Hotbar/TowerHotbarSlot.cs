using System;
using TowerCreep.Player.TowerCollection;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.Hotbar
{
    public class TowerHotbarSlot : MonoBehaviour
    {
        // Bubble up events
        public Action<TowerHotbarSlot> OnButtonSlotPressed;

        [SerializeField] private Sprite numberTexture;
        public TowerCollectionSlot CollectionSlot
        {
            get => _collectionSlot;
            set => SetCollectionData(value);
        }
        private TowerCollectionSlot _collectionSlot;

        // Internal Nodes
        [SerializeField] private Image selectedBorder;
        [SerializeField] private Image unselectedBorder;
        [SerializeField] private Image numberLabel;
        [SerializeField] private Image displayImage;

        // Internal State
        private bool isSelected;
        private bool isHovered;
        public bool IsAvailable { get; private set; }

        private void Start()
        {
            UpdateLabel();
            UpdateSelected();
        }


        private void HandleMouseEnter()
        {
            isHovered = true;
        }

        private void HandleMouseExit()
        {
            isHovered = false;
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
            if (numberLabel == null) return;

            numberLabel.sprite = numberTexture;
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
            if (selectedBorder == null || unselectedBorder == null) return;

            selectedBorder.enabled = isSelected;
            unselectedBorder.enabled = !isSelected;
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
                displayImage.sprite = _collectionSlot.CollectionTowerData.disabledTowerIcon;
            }
            else
            {
                displayImage.sprite = _collectionSlot.CollectionTowerData.towerIcon;
            }
        }
    }
}