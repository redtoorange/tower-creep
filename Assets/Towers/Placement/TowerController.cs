using System;
using System.Collections.Generic;
using TowerCreep.Interface.HotBar;
using TowerCreep.Levels.DungeonLevels;
using TowerCreep.Player;
using TowerCreep.Player.TowerCollection;
using UnityEngine;

namespace TowerCreep.Towers.Placement
{
    public class TowerController : MonoBehaviour
    {
        public static Action OnStopPlacingTower;
        public static Action<TowerCollectionSlot> OnSetTowerAsUsed;
        public static Action<TowerCollectionSlot> OnSetTowerAsAvailable;

        private BuildableTile hoveredTile;
        [SerializeField] private PlayerResourceManager playerResourceManager;

        private TowerCollectionSlot currentlySelectedTower;
        private bool isPlacingTower;

        private bool isValidPlacement = false;
        private bool tileIsDirty = false;
        [SerializeField] private SpriteRenderer validPlacementIcon;
        [SerializeField] private SpriteRenderer invalidPlacementIcon;

        private List<Tower> controlledTowers;

        private void Start()
        {
            controlledTowers = new List<Tower>();
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
            if (!isPlacingTower || currentlySelectedTower == null) return;

            // BuildableTile tempHovered = GetHoveredTile();
            // if (hoveredTile != tempHovered || tileIsDirty)
            // {
            //     hoveredTile = tempHovered;
            //     isValidPlacement = hoveredTile != null &&
            //                        !hoveredTile.isOccupied;
            //     UpdatePlacementIcon();
            //
            //     tileIsDirty = false;
            // }
        }

        // public override void _UnhandledInput(InputEvent @event)
        // {
        //     if (@event.IsActionPressed("BuildTower"))
        //     {
        //         PlaceTower();
        //     }
        //     else if (@event.IsActionPressed("CancelBuilding"))
        //     {
        //         StopPlacingTower();
        //     }
        // }

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
                validPlacementIcon.enabled = false;
                invalidPlacementIcon.enabled = false;
            }
            else
            {
                if (isValidPlacement)
                {
                    validPlacementIcon.enabled = true;
                    invalidPlacementIcon.enabled = false;
                    validPlacementIcon.transform.position =
                        hoveredTile.transform.position;
                }
                else
                {
                    validPlacementIcon.enabled = false;
                    invalidPlacementIcon.enabled = true;
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

        // private BuildableTile GetHoveredTile()
        // {
        //     World2D world = GetWorld2d();
        //     Array<Dictionary> results = new Array<Dictionary>(
        //         world.DirectSpaceState.IntersectPoint(GetLocalMousePosition(), collideWithAreas: true)
        //     );
        //     if (results.Count > 0)
        //     {
        //         for (int i = 0; i < results.Count; i++)
        //         {
        //             Godot.Collections.Dictionary<string, object> data =
        //                 new Godot.Collections.Dictionary<string, object>(results[i]);
        //             if (data["collider"] is BuildableTile buildableTile)
        //             {
        //                 return buildableTile;
        //             }
        //         }
        //     }
        //
        //     return null;
        // }

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