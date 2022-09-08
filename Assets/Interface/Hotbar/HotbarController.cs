using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers.Placement;

namespace TowerCreep.Interface.Hotbar
{
    public class HotbarController : Control
    {
        // External Events
        public static Action<TowerCollectionSlot> OnBuildingSelected;

        // External Nodes
        [Export] private NodePath buttonContainerPath;
        private List<TowerHotbarSlot> towerSlots;
        
        // Internal State
        private TowerHotbarSlot currentSlot;

        public override void _Ready()
        {
            List<TowerCollectionSlot> towerCollection = PlayerTowerCollection.S.GetTowerCollection();
            towerSlots = new List<TowerHotbarSlot>();
            Node buttonContainer = GetNode(buttonContainerPath);
            for (int i = 0; i < buttonContainer.GetChildCount(); i++)
            {
                if (buttonContainer.GetChild(i) is TowerHotbarSlot hbs)
                {
                    towerSlots.Add(hbs);
                    hbs.OnButtonSlotPressed += HandleOnButtonSlotPressed;
                    if (towerCollection != null && i < towerCollection.Count)
                    {
                        TowerCollectionSlot tcs = towerCollection[i];
                        hbs.CollectionSlot = tcs;
                        tcs.CollectionHotBarSlot = hbs;
                    }
                }
            }

            TowerController.OnStopPlacingTower += HandleStopPlacingTower;
            TowerController.OnSetTowerAsAvailable += (slot) => HandleSetTowerAsAvailable(slot, true);
            TowerController.OnSetTowerAsUsed += (slot) => HandleSetTowerAsAvailable(slot, false);
        }

        private void HandleSetTowerAsAvailable(TowerCollectionSlot collectionSlot, bool available)
        {
            if (collectionSlot.CollectionHotBarSlot != null)
            {
                collectionSlot.CollectionHotBarSlot.SetAvailable(available);
            }
        }

        private void HandleStopPlacingTower()
        {
            HandleOnButtonSlotPressed(null);
        }

        private void HandleOnButtonSlotPressed(TowerHotbarSlot newSlot)
        {
            if (newSlot != currentSlot)
            {
                if (currentSlot != null)
                {
                    currentSlot.SetSelected(false);
                }

                currentSlot = newSlot;

                if (currentSlot != null)
                {
                    currentSlot.SetSelected(true);
                    OnBuildingSelected?.Invoke(currentSlot.CollectionSlot);
                }
            }
        }

        public override void _Input(InputEvent @event)
        {
            for (int i = 0; i < 9; i++)
            {
                TowerHotbarSlot current = towerSlots[i];
                if (@event.IsActionPressed(current.Name) && current.IsAvailable)
                {
                    HandleOnButtonSlotPressed(towerSlots[i]);
                    return;
                }
            }
        }
    }
}