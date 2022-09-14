using System;
using System.Collections.Generic;
using TowerCreep.Interface.HotBar;
using TowerCreep.Levels.DungeonLevels;
using TowerCreep.Player.TowerCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Towers.Placement
{
    public class TowerController : MonoBehaviour
    {
        public static Action OnStopPlacingTower;
        public static Action<TowerCollectionSlot> OnSetTowerAsUsed;
        public static Action<TowerCollectionSlot> OnSetTowerAsAvailable;

        private BuildableTile hoveredTile;
        private TowerCollectionSlot currentlySelectedTower;
        private bool isPlacingTower;

        private bool isValidPlacement = false;
        private bool tileIsDirty = false;

        [SerializeField] private GameObject validPlacementIcon;
        [SerializeField] private GameObject invalidPlacementIcon;
        [SerializeField] private ContactFilter2D buildableTileFilter;

        private List<Tower> controlledTowers;
        private GameInputActions inputActions;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            controlledTowers = new List<Tower>();
            
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
            DungeonLevel.OnPlayerExitedLevel += HandlePlayerExitedLevel;
        }


        private void OnDisable()
        {
            TowerHotBarController.OnBuildingSelected -= HandleEnterBuildingPlaceMode;
            DungeonLevel.OnPlayerExitedLevel -= HandlePlayerExitedLevel;
        }

        private void HandleEnterBuildingPlaceMode(TowerCollectionSlot selectedTower)
        {
            currentlySelectedTower = selectedTower;
            isPlacingTower = true;
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
            if (isValidPlacement && currentlySelectedTower != null)
            {
                hoveredTile.isOccupied = true;

                Tower tower = Instantiate(currentlySelectedTower.CollectionTowerData.towerPrefab, transform);
                tower.transform.position = hoveredTile.transform.position;
                controlledTowers.Add(tower);

                currentlySelectedTower.IsPlaced = true;
                tower.CollectionSlotData = currentlySelectedTower;
                OnSetTowerAsUsed?.Invoke(currentlySelectedTower);

                currentlySelectedTower = null;
                tileIsDirty = true;
                StopPlacingTower();
            }
        }

        private void DeconstructTower(Tower which)
        {
            TowerCollectionSlot collectionSlot = which.CollectionSlotData;
            collectionSlot.IsPlaced = false;
            OnSetTowerAsAvailable?.Invoke(collectionSlot);
        }

        private void HandlePlayerExitedLevel(DungeonLevel level)
        {
            for (int i = 0; i < controlledTowers.Count; i++)
            {
                DeconstructTower(controlledTowers[i]);
                Destroy(controlledTowers[i].gameObject);
            }

            controlledTowers.Clear();
        }
    }
}