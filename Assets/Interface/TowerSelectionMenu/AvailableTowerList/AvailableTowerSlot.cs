using System;
using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerSlot : HoverableSlot
    {
        public static Action<AvailableTowerSlot> OnAddToSelected;
        public static Action<AvailableTowerSlot> OnOpenManual;

        [Export] private TowerData towerData;
        [Export] private PackedScene previewPrefab;

        private TextureRect slotSprite;

        public override void _Ready()
        {
            base._Ready();

            slotSprite = GetNode<TextureRect>("SlotSprite");
            if (towerData != null)
            {
                slotSprite.Texture = towerData.towerIcon;
                HintTooltip = towerData.towerName;
            }
        }

        public override object GetDragData(Vector2 position)
        {
            TextureRect tr = new TextureRect();
            tr.Texture = towerData.towerIcon;
            SetDragPreview(tr);
            return new TowerSelectionPayload(towerData);
        }

        protected override void HandleDoubleLeftClicked() => OnAddToSelected?.Invoke(this);

        protected override void HandleRightClicked()
        {
            if (towerData != null)
            {
                OnOpenManual?.Invoke(this);
            }
        }

        public TowerData GetTowerData() => towerData;
    }
}