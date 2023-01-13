using TowerCreep.TowerCreep2D.Scripts.Towers.Placement;
using TowerCreep.TowerCreep2D.Scripts.Towers.Selection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.TowerCreep2D.Scripts.Towers
{
    public enum PlayerInputMode
    {
        Selection,
        Placement
    }

    public class PlayerInputModeController : MonoBehaviour
    {
        [SerializeField] private TowerSelectionController towerSelectionController;
        [SerializeField] private TowerPlacementController towerPlacementController;

        private PlayerInputMode currentInputMode = PlayerInputMode.Selection;
        private GameInputActions inputActions;

        private void Start()
        {
            TowerPlacementController.OnStartPlacingTower += StartPlacingMode;
            TowerPlacementController.OnStopPlacingTower += StopPlacingMode;

            inputActions = new GameInputActions();
            inputActions.PlayerActions.StopBuilding.performed += HandleRightClick;
            inputActions.PlayerActions.PlaceBuilding.performed += HandleLeftClick;
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
            TowerPlacementController.OnStartPlacingTower -= StartPlacingMode;
            TowerPlacementController.OnStopPlacingTower -= StopPlacingMode;
        }

        private void StopPlacingMode()
        {
            currentInputMode = PlayerInputMode.Selection;
        }

        private void StartPlacingMode()
        {
            currentInputMode = PlayerInputMode.Placement;
        }


        private void HandleLeftClick(InputAction.CallbackContext obj)
        {
            if (currentInputMode == PlayerInputMode.Placement)
            {
                towerPlacementController.HandleLeftClick(obj);
            }
            else
            {
                towerSelectionController.HandleLeftClick(obj);
            }
        }

        private void HandleRightClick(InputAction.CallbackContext obj)
        {
            if (currentInputMode == PlayerInputMode.Placement)
            {
                towerPlacementController.HandleRightClick(obj);
            }
            else
            {
                towerSelectionController.HandleRightClick(obj);
            }
        }

        private void Update()
        {
            if (currentInputMode == PlayerInputMode.Placement)
            {
                towerPlacementController.ProcessUpdate();
            }
            else
            {
                towerSelectionController.ProcessUpdate();
            }
        }
    }
}