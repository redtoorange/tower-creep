using System;
using System.Collections.Generic;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Towers.Placement;
using UnityEngine;

namespace TowerCreep.Interface.HotBar
{
    public class TowerHotBarController : MonoBehaviour
    {
        // External Events
        public static Action<TowerCollectionSlot> OnBuildingSelected;

        // External Nodes
        [SerializeField] private List<TowerHotBarSlot> towerSlots;

        // Internal State
        private TowerHotBarSlot currentSlot;

        private void Start()
        {
            List<TowerCollectionSlot> towerCollection = PlayerTowerCollectionManager.S.GetTowerCollection();
            for (int i = 0; i < towerSlots.Count; i++)
            {
                TowerHotBarSlot hbs = towerSlots[i];
                hbs.OnButtonSlotPressed += HandleOnButtonSlotPressed;
                if (towerCollection != null && i < towerCollection.Count)
                {
                    TowerCollectionSlot tcs = towerCollection[i];
                    hbs.CollectionSlot = tcs;
                    tcs.CollectionHotBarSlot = hbs;
                }
            }

            TowerPlacementController.OnStopPlacingTower += HandleStopPlacingTower;
            TowerController.OnSetTowerAsAvailable += (slot) => HandleSetTowerAsAvailable(slot, true);
            TowerPlacementController.OnSetTowerAsUsed += (slot) => HandleSetTowerAsAvailable(slot, false);
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

        private void HandleOnButtonSlotPressed(TowerHotBarSlot newSlot)
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

        // public override void _Input(InputEvent @event)
        // {
        //     for (int i = 0; i < 9; i++)
        //     {
        //         TowerHotBarSlot current = towerSlots[i];
        //         if (@event.IsActionPressed(current.Name) && current.IsAvailable)
        //         {
        //             HandleOnButtonSlotPressed(towerSlots[i]);
        //             return;
        //         }
        //     }
        // }
    }
}