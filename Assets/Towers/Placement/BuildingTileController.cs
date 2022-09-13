using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerCreep.Towers.Placement
{
    /// <summary>
    /// Scan the map on first load and replace the "Buildable" tilemap with instances of the BuildableTileNode.
    /// </summary>
    public class BuildingTileController : MonoBehaviour
    {
        [SerializeField] private BuildableTile buildingSlotPrefab;
        [SerializeField] private Tilemap buildableTilemap;

        private void Start()
        {
            BoundsInt bounds = buildableTilemap.cellBounds;
            for (int x = bounds.x; x < bounds.size.x; x++)
            {
                for (int y = bounds.y; y < bounds.size.y; y++)
                {
                    Vector3Int pos = new(x, y);
                    if (buildableTilemap.HasTile(pos))
                    {
                        Instantiate(buildingSlotPrefab,
                            buildableTilemap.GetCellCenterWorld(pos),
                            Quaternion.identity,
                            transform
                        );
                    }
                }
            }

            buildableTilemap.gameObject.SetActive(false);
        }
    }
}