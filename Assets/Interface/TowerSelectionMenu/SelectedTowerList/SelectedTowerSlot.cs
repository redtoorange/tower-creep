using System;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.SelectedTowerList
{
    public class SelectedTowerSlot : HoverableSlot
    {
        public static Action<SelectedTowerSlot> OnRemoveFromSelected;
        public static Action<SelectedTowerSlot> OnOpenManual;
        public Action OnTowerDataChanged;

        [SerializeField] private Sprite defaultSlotTexture;
        [SerializeField] private GameObject previewPrefab;

        private TowerData towerData;
        [SerializeField] private Image towerDisplay;

        private void Start()
        {
            towerDisplay.sprite = defaultSlotTexture;
        }

        public object GetDragData(Vector2 position)
        {
            if (towerData == null) return null;

            // TextureRect tr = new TextureRect();
            // tr.Texture = towerData.towerIcon;
            // SetDragPreview(tr);
            return new TowerSwapPayload(towerData, this);
        }

        public bool CanDropData(Vector2 position, object data)
        {
            return data is TowerSelectionPayload || data is TowerSwapPayload;
        }

        public void DropData(Vector2 position, object data)
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
            towerDisplay.sprite = defaultSlotTexture;
            // HintTooltip = null;
            OnTowerDataChanged?.Invoke();
        }

        public void SetTowerData(TowerData data)
        {
            towerData = data;
            towerDisplay.sprite = towerData.towerIcon;
            // HintTooltip = data.towerName;
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