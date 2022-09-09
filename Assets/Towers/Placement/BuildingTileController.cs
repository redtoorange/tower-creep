using UnityEngine;

namespace TowerCreep.Towers.Placement
{
    /// <summary>
    /// Scan the map on first load and replace the "Buildable" tilemap with instances of the BuildableTileNode.
    /// </summary>
    public class BuildingTileController : MonoBehaviour
    {
        [SerializeField] private BuildableTile buildingSlotPrefab;

        private void Start()
        {
            // TileMap buildingSlotMap = GetChild(0) as TileMap;
            // Array<Vector2> usedCells = new Array<Vector2>(buildingSlotMap.GetUsedCells());
            // for (int i = 0; i < usedCells.Count; i++)
            // {
            //     BuildableTile buildSlot = Instantiate(buildingSlotPrefab, transform);
            //     buildSlot.Position = buildingSlotMap.MapToWorld(usedCells[i]) + new Vector2(8, 8);
            //     buildSlot.Initialize(new Vector2(8, 8));
            // }
            //
            // buildingSlotMap.QueueFree();
        }
    }
}