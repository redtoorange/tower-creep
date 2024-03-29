﻿using System;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers.Selection;
using UnityEngine;

namespace TowerCreep.Towers
{
    public class Tower : MonoBehaviour, ISelectable, IHoverable
    {
        public static Action<TowerSelectionStateChange> OnTowerSelectionStateChange;

        public Action OnTowerPlaced;
        public Action OnTowerRemoved;
        public Action OnTowerSelected;
        public Action OnTowerDeselected;

        [SerializeField] private SpriteRenderer towerSprite;
        [SerializeField] private SpriteRenderer hoveredSprite;
        [SerializeField] private SpriteRenderer selectionSprite;
        private PlayerPartySlot collectionSlot;

        private TowerSelectionState selectionState = TowerSelectionState.DeSelected;
        private TowerHoverState hoverState = TowerHoverState.UnHovered;

        public void PlaceTower(PlayerPartySlot collectionSlot)
        {
            this.collectionSlot = collectionSlot;
            this.collectionSlot.IsPlaced = true;

            towerSprite.sprite = this.collectionSlot.CollectionTowerData.towerIcon;

            OnTowerPlaced?.Invoke();
        }

        public void RemoveTower()
        {
            collectionSlot.IsPlaced = false;
            OnTowerRemoved?.Invoke();
        }

        public PlayerPartySlot GetCollectionSlotData()
        {
            return collectionSlot;
        }

        public void SetSelected(bool isSelected)
        {
            TowerSelectionState newState = isSelected ? TowerSelectionState.Selected : TowerSelectionState.DeSelected;
            if (newState != selectionState)
            {
                OnTowerSelectionStateChange?.Invoke(new TowerSelectionStateChange(
                    this, hoverState, hoverState, selectionState, newState
                ));
                selectionState = newState;
                UpdateVisual();

                if (selectionState == TowerSelectionState.Selected)
                {
                    OnTowerSelected?.Invoke();
                }
                else
                {
                    OnTowerDeselected?.Invoke();
                }
            }
        }

        public void SetHovered(bool isHovered)
        {
            TowerHoverState newState = isHovered ? TowerHoverState.Hovered : TowerHoverState.UnHovered;
            if (newState != hoverState)
            {
                OnTowerSelectionStateChange?.Invoke(new TowerSelectionStateChange(
                    this, hoverState, newState, selectionState, selectionState
                ));
                hoverState = newState;
                UpdateVisual();
            }
        }

        private void UpdateVisual()
        {
            selectionSprite.enabled = false;
            hoveredSprite.enabled = false;

            if (selectionState == TowerSelectionState.Selected)
            {
                selectionSprite.enabled = true;
            }
            else if (hoverState == TowerHoverState.Hovered)
            {
                hoveredSprite.enabled = true;
            }
        }

        public void RewardExperience(int amount)
        {
            collectionSlot.TowerInstanceProgressionData.GiveExperience(amount);
        }
    }
}