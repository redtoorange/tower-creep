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
        private GameInputActions inputActions;

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

            SetupInput();
        }

        private void SetupInput()
        {
            inputActions = new GameInputActions();
            inputActions.BuildBarKeys.Slot_1.performed += _ => HandleOnButtonSlotPressed(towerSlots[0]);
            inputActions.BuildBarKeys.Slot_2.performed += _ => HandleOnButtonSlotPressed(towerSlots[1]);
            inputActions.BuildBarKeys.Slot_3.performed += _ => HandleOnButtonSlotPressed(towerSlots[2]);
            inputActions.BuildBarKeys.Slot_4.performed += _ => HandleOnButtonSlotPressed(towerSlots[3]);
            inputActions.BuildBarKeys.Slot_5.performed += _ => HandleOnButtonSlotPressed(towerSlots[4]);
            inputActions.BuildBarKeys.Slot_6.performed += _ => HandleOnButtonSlotPressed(towerSlots[5]);
            inputActions.BuildBarKeys.Slot_7.performed += _ => HandleOnButtonSlotPressed(towerSlots[6]);
            inputActions.BuildBarKeys.Slot_8.performed += _ => HandleOnButtonSlotPressed(towerSlots[7]);
            inputActions.BuildBarKeys.Slot_9.performed += _ => HandleOnButtonSlotPressed(towerSlots[8]);
            inputActions.Enable();
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

                if (currentSlot != null && currentSlot.IsAvailable)
                {
                    currentSlot.SetSelected(true);
                    OnBuildingSelected?.Invoke(currentSlot.CollectionSlot);
                }
            }
        }
    }
}