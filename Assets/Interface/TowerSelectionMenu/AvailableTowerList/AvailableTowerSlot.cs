using System;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu.AvailableTowerList
{
    public class AvailableTowerSlot : HoverableSlot
    {
        public static Action<AvailableTowerSlot> OnAddToSelected;
        public static Action<AvailableTowerSlot> OnOpenManual;

        [SerializeField] private TowerData towerData;
        [SerializeField] private GameObject previewPrefab;

        private Image slotSprite;

        private void Start()
        {
            if (towerData != null)
            {
                slotSprite.sprite = towerData.towerIcon;
            }
        }

        public object GetDragData(Vector2 position)
        {
            // TextureRect tr = new TextureRect();
            // tr.Texture = towerData.towerIcon;
            // SetDragPreview(tr);
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