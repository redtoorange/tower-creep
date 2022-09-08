using System;
using Godot;
using TowerCreep.Towers;

namespace TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerSlot : HoverableSlot
    {
        public static Action<SelectedTowerSlot> OnRemoveFromSelected;
        public static Action<SelectedTowerSlot> OnOpenManual;
        public Action OnTowerDataChanged;

        [Export] private Texture defaultSlotTexture;
        [Export] private PackedScene previewPrefab;

        private TowerData towerData;
        private TextureRect towerDisplay;

        public override void _Ready()
        {
            base._Ready();

            towerDisplay = GetNode<TextureRect>("TextureRect");
            towerDisplay.Texture = defaultSlotTexture;
        }

        public override object GetDragData(Vector2 position)
        {
            if (towerData == null) return null;

            TextureRect tr = new TextureRect();
            tr.Texture = towerData.towerIcon;
            SetDragPreview(tr);
            return new TowerSwapPayload(towerData, this);
        }

        public override bool CanDropData(Vector2 position, object data)
        {
            return data is TowerSelectionPayload || data is TowerSwapPayload;
        }

        public override void DropData(Vector2 position, object data)
        {
            if (data is TowerSelectionPayload payload)
            {
                SetTowerData(payload.towerData);
            }
            else if (data is TowerSwapPayload swapPayload)
            {
                if (towerData == null)
                {
                    swapPayload.sourceSlot.ClearTowerData();
                }
                else
                {
                    swapPayload.sourceSlot.SetTowerData(towerData);
                }

                SetTowerData(swapPayload.towerData);
            }
        }

        public void ClearTowerData()
        {
            towerData = null;
            towerDisplay.Texture = defaultSlotTexture;
            HintTooltip = null;
            OnTowerDataChanged?.Invoke();
        }

        public void SetTowerData(TowerData data)
        {
            towerData = data;
            towerDisplay.Texture = towerData.towerIcon;
            HintTooltip = data.towerName;
            OnTowerDataChanged?.Invoke();
        }

        protected override void HandleDoubleLeftClicked() => OnRemoveFromSelected?.Invoke(this);

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