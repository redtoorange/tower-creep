using TowerCreep.Towers.Placement;
using TowerCreep.Towers.Selection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Towers
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
            TowerPlacementController.OnStartPlacingTower += () => currentInputMode = PlayerInputMode.Placement;
            TowerPlacementController.OnStopPlacingTower += () => currentInputMode = PlayerInputMode.Selection;

            inputActions = new GameInputActions();
            
            inputActions = new GameInputActions();
            inputActions.Enable();

            inputActions.PlayerActions.StopBuilding.performed += HandleRightClick;
            inputActions.PlayerActions.PlaceBuilding.performed += HandleLeftClick;
            
            inputActions.Enable();
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