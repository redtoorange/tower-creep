using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using TowerCreep.Interface.Hotbar;
using TowerCreep.Levels.DungeonLevels;
using TowerCreep.Player;
using TowerCreep.Player.TowerCollection;

namespace TowerCreep.Towers.Placement
{
    public class TowerController : Node2D
    {
        public static Action OnStopPlacingTower;
        public static Action<TowerCollectionSlot> OnSetTowerAsUsed;
        public static Action<TowerCollectionSlot> OnSetTowerAsAvailable;
        
        [Export] private NodePath playerResourceManagerPath;

        private BuildableTile hoveredTile;
        private PlayerResourceManager playerResourceManager;

        private TowerCollectionSlot currentlySelectedTower;
        private bool isPlacingTower;

        private bool isValidPlacement = false;
        private bool tileIsDirty = false;
        private Sprite validPlacementIcon;
        private Sprite invalidPlacementIcon;

        private List<Tower> controlledTowers;

        public override void _Ready()
        {
            controlledTowers = new List<Tower>();
            playerResourceManager = GetNode<PlayerResourceManager>(playerResourceManagerPath);

            invalidPlacementIcon = GetNode<Sprite>("InvalidPlacement");
            validPlacementIcon = GetNode<Sprite>("ValidPlacement");
        }

        public override void _EnterTree()
        {
            HotbarController.OnBuildingSelected += HandleEnterBuildingPlaceMode;
            DungeonLevel.OnPlayerExitedLevel += HandlePlayerExitedLevel;
        }


        public override void _ExitTree()
        {
            HotbarController.OnBuildingSelected -= HandleEnterBuildingPlaceMode;
            DungeonLevel.OnPlayerExitedLevel -= HandlePlayerExitedLevel;
        }

        private void HandleEnterBuildingPlaceMode(TowerCollectionSlot selectedTower)
        {
            currentlySelectedTower = selectedTower;
            isPlacingTower = true;
        }

        public override void _Process(float delta)
        {
            if (!isPlacingTower || currentlySelectedTower == null) return;

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

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("BuildTower"))
            {
                PlaceTower();
            }
            else if (@event.IsActionPressed("CancelBuilding"))
            {
                StopPlacingTower();
            }
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
                validPlacementIcon.Visible = false;
                invalidPlacementIcon.Visible = false;
            }
            else
            {
                if (isValidPlacement)
                {
                    validPlacementIcon.Visible = true;
                    invalidPlacementIcon.Visible = false;
                    validPlacementIcon.GlobalPosition =
                        hoveredTile.GlobalPosition;
                }
                else
                {
                    validPlacementIcon.Visible = false;
                    invalidPlacementIcon.Visible = true;
                    invalidPlacementIcon.GlobalPosition =
                        hoveredTile.GlobalPosition;
                }
            }
        }

        private void PlaceTower()
        {
            if (isValidPlacement && currentlySelectedTower != null)
            {
                hoveredTile.isOccupied = true;

                Tower tower = currentlySelectedTower.CollectionTowerData.towerPrefab.Instance<Tower>();
                AddChild(tower);
                tower.GlobalPosition = hoveredTile.GlobalPosition;
                controlledTowers.Add(tower);

                currentlySelectedTower.IsPlaced = true;
                tower.CollectionSlotData = currentlySelectedTower;
                OnSetTowerAsUsed?.Invoke(currentlySelectedTower);
                
                currentlySelectedTower = null;
                tileIsDirty = true;
                StopPlacingTower();
            }
        }

        private BuildableTile GetHoveredTile()
        {
            World2D world = GetWorld2d();
            Array<Dictionary> results = new Array<Dictionary>(
                world.DirectSpaceState.IntersectPoint(GetLocalMousePosition(), collideWithAreas: true)
            );
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Godot.Collections.Dictionary<string, object> data =
                        new Godot.Collections.Dictionary<string, object>(results[i]);
                    if (data["collider"] is BuildableTile buildableTile)
                    {
                        return buildableTile;
                    }
                }
            }

            return null;
        }

        private void DeconstructTower(Tower which)
        {
            // which.CollectionSlotData
            TowerCollectionSlot collectionSlot = which.CollectionSlotData;
            collectionSlot.IsPlaced = false;
            OnSetTowerAsAvailable?.Invoke(collectionSlot);
        }

        private void HandlePlayerExitedLevel(DungeonLevel level)
        {
            for (int i = 0; i < controlledTowers.Count; i++)
            {
                DeconstructTower(controlledTowers[i]);
                controlledTowers[i].QueueFree();
            }

            controlledTowers.Clear();
        }
    }
}