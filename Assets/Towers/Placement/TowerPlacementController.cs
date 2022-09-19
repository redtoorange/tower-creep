using System;
using TowerCreep.Interface.HotBar;
using TowerCreep.Player.TowerCollection;
using TowerCreep.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Towers.Placement
{
    public class TowerPlacementController : MonoBehaviour
    {
        public static Action OnStopPlacingTower;
        public static Action<TowerCollectionSlot> OnSetTowerAsUsed;

        private BuildableTile hoveredTile;
        private TowerCollectionSlot currentlySelectedTower;
        private bool isPlacingTower;

        private bool isValidPlacement = false;
        private bool tileIsDirty = false;

        [SerializeField] private GameObject validPlacementIcon;
        [SerializeField] private GameObject invalidPlacementIcon;
        [SerializeField] private ContactFilter2D buildableTileFilter;

        private GameInputActions inputActions;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            inputActions = new GameInputActions();
            inputActions.Enable();

            inputActions.PlayerActions.StopBuilding.performed += HandleStopBuildingPressed;
            inputActions.PlayerActions.PlaceBuilding.performed += HandlePlaceBuildingPressed;
        }

        private void HandlePlaceBuildingPressed(InputAction.CallbackContext obj)
        {
            if (isPlacingTower)
            {
                PlaceTower();
            }
        }

        private void HandleStopBuildingPressed(InputAction.CallbackContext obj)
        {
            if (isPlacingTower)
            {
                StopPlacingTower();
            }
        }

        private void OnEnable()
        {
            TowerHotBarController.OnBuildingSelected += HandleEnterBuildingPlaceMode;
        }


        private void OnDisable()
        {
            TowerHotBarController.OnBuildingSelected -= HandleEnterBuildingPlaceMode;
        }

        private void HandleEnterBuildingPlaceMode(TowerCollectionSlot selectedTower)
        {
            if (!selectedTower.IsPlaced)
            {
                currentlySelectedTower = selectedTower;
                isPlacingTower = true;
            }
            else
            {
                currentlySelectedTower = null;
                isPlacingTower = false;
            }
        }

        private void Update()
        {
            // if (!isPlacingTower || currentlySelectedTower == null) return;

            if (!isPlacingTower) return;

            BuildableTile tempHovered = GetHoveredTile();
            if (hoveredTile != tempHovered || tileIsDirty)
            {
                hoveredTile = tempHovered;
                isValidPlacement = hoveredTile != null &&
                                   !hoveredTile.isOccupied;
                UpdatePlacementIcon();

                tileIsDirty = false;
            }
        }

        private BuildableTile GetHoveredTile()
        {
            Collider2D[] collisions = new Collider2D[1];
            int colliderCount = Physics2D.OverlapPoint(
                mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                buildableTileFilter,
                collisions
            );

            if (colliderCount > 0 && collisions[0].TryGetComponent(out BuildableTile bt))
            {
                return bt;
            }

            return null;
        }

        private void StopPlacingTower()
        {
            currentlySelectedTower = null;
            hoveredTile = null;
            isPlacingTower = false;
            UpdatePlacementIcon();
            OnStopPlacingTower?.Invoke();
        }

        private void UpdatePlacementIcon()
        {
            if (hoveredTile == null)
            {
                validPlacementIcon.SetActive(false);
                invalidPlacementIcon.SetActive(false);
            }
            else
            {
                if (isValidPlacement)
                {
                    validPlacementIcon.SetActive(true);
                    invalidPlacementIcon.SetActive(false);
                    validPlacementIcon.transform.position =
                        hoveredTile.transform.position;
                }
                else
                {
                    validPlacementIcon.SetActive(false);
                    invalidPlacementIcon.SetActive(true);
                    invalidPlacementIcon.transform.position =
                        hoveredTile.transform.position;
                }
            }
        }

        private void PlaceTower()
        {
            if (!isValidPlacement || currentlySelectedTower == null)
            {
                return;
            }

            hoveredTile.isOccupied = true;

            Tower tower = Instantiate(currentlySelectedTower.CollectionTowerData.towerPrefab, transform);
            tower.transform.position = hoveredTile.transform.position;
            hoveredTile.towerController.AddTower(tower);

            currentlySelectedTower.IsPlaced = true;
            tower.PlaceTower(currentlySelectedTower);
            OnSetTowerAsUsed?.Invoke(currentlySelectedTower);

            currentlySelectedTower = null;
            tileIsDirty = true;
            StopPlacingTower();
        }
    }
}