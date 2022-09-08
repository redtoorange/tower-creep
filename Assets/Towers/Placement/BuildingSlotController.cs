using Godot;
using Godot.Collections;

namespace TowerCreep.Towers.Placement
{
    /// <summary>
    /// Scan the map on first load and replace the "Buildable" tilemap with instances of the BuildableTileNode.
    /// </summary>
    public class BuildingSlotController : Node2D
    {
        [Export] private PackedScene buildingSlotPrefab;

        public override void _Ready()
        {
            TileMap buildingSlotMap = GetChild(0) as TileMap;

            Array<Vector2> usedCells = new Array<Vector2>(buildingSlotMap.GetUsedCells());
            for (int i = 0; i < usedCells.Count; i++)
            {
                BuildableTile buildSlot = buildingSlotPrefab.Instance<BuildableTile>();
                buildSlot.Position = buildingSlotMap.MapToWorld(usedCells[i]) + new Vector2(8, 8);
                buildSlot.Initialize(new Vector2(8, 8));
                AddChild(buildSlot);
            }

            buildingSlotMap.QueueFree();
        }
    }
}