using TowerCreep.Towers.Placement;
using TowerCreep.Towers.Selection;
using UnityEngine;

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

        private void Start()
        {
            TowerPlacementController.OnStartPlacingTower += () => currentInputMode = PlayerInputMode.Placement;
            TowerPlacementController.OnStopPlacingTower += () => currentInputMode = PlayerInputMode.Selection;
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