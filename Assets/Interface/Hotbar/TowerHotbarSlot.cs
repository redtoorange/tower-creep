using System;
using Godot;
using TowerCreep.Player.TowerCollection;

namespace TowerCreep.Interface.Hotbar
{
    public class TowerHotbarSlot : Control
    {
        // Bubble up events
        public Action<TowerHotbarSlot> OnButtonSlotPressed;

        [Export] private Texture numberTexture;
        public TowerCollectionSlot CollectionSlot
        {
            get => _collectionSlot;
            set => SetCollectionData(value);
        }
        private TowerCollectionSlot _collectionSlot;

        // Internal Nodes
        private TextureRect selectedBorder;
        private TextureRect unselectedBorder;
        private TextureRect numberLabel;
        private TextureRect displayImage;

        // Internal State
        private bool isSelected;
        private bool isHovered;
        public bool IsAvailable { get; private set; }


        public override void _Ready()
        {
            selectedBorder = GetNode<TextureRect>("SelectedBorder");
            unselectedBorder = GetNode<TextureRect>("UnSelectedBorder");
            numberLabel = GetNode<TextureRect>("NumberLabel");
            displayImage = GetNode<TextureRect>("DisplayImage");

            UpdateLabel();
            UpdateSelected();

            Connect("mouse_entered", this, nameof(HandleMouseEnter));
            Connect("mouse_exited", this, nameof(HandleMouseExit));
        }


        private void HandleMouseEnter()
        {
            isHovered = true;
        }

        private void HandleMouseExit()
        {
            isHovered = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (!isHovered || !IsAvailable) return;

            if (@event is InputEventMouseButton mb)
            {
                if (mb.Pressed && mb.ButtonIndex == (int)ButtonList.Left)
                {
                    OnButtonSlotPressed?.Invoke(this);
                }
            }
        }

        private void UpdateLabel()
        {
            if (numberLabel == null) return;

            numberLabel.Texture = numberTexture;
        }

        private void SetCollectionData(TowerCollectionSlot collectionSlot)
        {
            _collectionSlot = collectionSlot;
            HintTooltip = _collectionSlot.CollectionTowerData.towerName;
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

            selectedBorder.Visible = isSelected;
            unselectedBorder.Visible = !isSelected;
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
                displayImage.Texture = _collectionSlot.CollectionTowerData.disabledTowerIcon;
            }
            else
            {
                displayImage.Texture = _collectionSlot.CollectionTowerData.towerIcon;
            }
        }
    }
}