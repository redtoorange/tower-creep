using System;
using TowerCreep.TowerCreep2D.Scripts.Interface.HotBar;
using TowerCreep.TowerCreep2D.Scripts.Player.TowerCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.Placement
{
    public class TowerPlacementController : MonoBehaviour
    {
        public static Action OnStartPlacingTower;
        public static Action OnStopPlacingTower;
        public static Action<TowerCollectionSlot> OnSetTowerAsUsed;

        private BuildableTile hoveredTile;
        private TowerCollectionSlot currentlySelectedTower;
        private bool isPlacingTower;

        private bool isValidPlacement = false;
        private bool tileIsDirty = false;

        [SerializeField] private ContactFilter2D buildableTileFilter;
        [SerializeField] private TowerPlacementGhost towerPlacementGhost;


        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public void HandleLeftClick(InputAction.CallbackContext obj)
        {
            if (isPlacingTower)
            {
                PlaceTower();
            }
        }

        public void HandleRightClick(InputAction.CallbackContext obj)
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
                towerPlacementGhost.SetData(currentlySelectedTower);
                OnStartPlacingTower?.Invoke();
                isPlacingTower = true;
            }
            else
            {
                currentlySelectedTower = null;
                isPlacingTower = false;
                OnStopPlacingTower?.Invoke();
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
            if (ReferenceEquals(hoveredTile, null))
            {
                towerPlacementGhost.SetShown(false);
            }
            else
            {
                towerPlacementGhost.SetShown(true);
                towerPlacementGhost.SetValid(isValidPlacement);
                towerPlacementGhost.SetPosition(hoveredTile.transform.position);
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

        public void ProcessUpdate()
        {
            BuildableTile tempHovered = GetHoveredTile();
            if (hoveredTile != tempHovered || tileIsDirty)
            {
                hoveredTile = tempHovered;
                isValidPlacement = !ReferenceEquals(hoveredTile, null) &&
                                   !hoveredTile.isOccupied;
                UpdatePlacementIcon();

                tileIsDirty = false;
            }
        }
    }
}