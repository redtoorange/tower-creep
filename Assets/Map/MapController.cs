using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace TowerCreep.Map
{
    public class MapController : Node2D
    {
        private float tileSize = 16.0f;
        private List<TileMap> tileMaps;
        private AStar2D aStar;
        private Rect2 mapBounds;
        private int mapWidth;
        private int mapHeight;
        private MapTile[,] compositeMap;

        public override void _Ready()
        {
            aStar = new AStar2D();

            CollectTileMaps();
            CalculateMapBounds();
            ConstructCompositeMap();
            SetWalls();
            BuildNodeGraph();
        }


        public void SetTileOccupied(MapTile tile, bool isOccupied)
        {
            tile.isOccupied = true;
            aStar.SetPointDisabled(tile.pointId, isOccupied);
        }

        private void BuildNodeGraph()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    MapTile tile = compositeMap[x, y];
                    if (tile != null && !tile.isWall)
                    {
                        // Left
                        if (x > 1)
                        {
                            MapTile neighbor = compositeMap[x - 1, y];
                            if (!neighbor.isWall)
                            {
                                aStar.ConnectPoints(tile.pointId, neighbor.pointId);
                            }

                            // Up
                            if (y > 1)
                            {
                                MapTile neighbor2 = compositeMap[x - 1, y - 1];
                                if (!neighbor.isWall)
                                {
                                    aStar.ConnectPoints(tile.pointId, neighbor2.pointId);
                                }
                            }

                            // Down
                            if (y < mapHeight - 1)
                            {
                                MapTile neighbor2 = compositeMap[x - 1, y + 1];
                                if (!neighbor.isWall)
                                {
                                    aStar.ConnectPoints(tile.pointId, neighbor2.pointId);
                                }
                            }
                        }

                        // Right
                        if (x < mapWidth - 1)
                        {
                            MapTile neighbor = compositeMap[x + 1, y];
                            if (!neighbor.isWall)
                            {
                                aStar.ConnectPoints(tile.pointId, neighbor.pointId);
                            }

                            // Up
                            if (y > 1)
                            {
                                MapTile neighbor2 = compositeMap[x + 1, y - 1];
                                if (!neighbor.isWall)
                                {
                                    aStar.ConnectPoints(tile.pointId, neighbor2.pointId);
                                }
                            }

                            // Down
                            if (y < mapHeight - 1)
                            {
                                MapTile neighbor2 = compositeMap[x + 1, y + 1];
                                if (!neighbor.isWall)
                                {
                                    aStar.ConnectPoints(tile.pointId, neighbor2.pointId);
                                }
                            }
                        }

                        // Up
                        if (y > 1)
                        {
                            MapTile neighbor = compositeMap[x, y - 1];
                            if (!neighbor.isWall)
                            {
                                aStar.ConnectPoints(tile.pointId, neighbor.pointId);
                            }
                        }

                        // Down
                        if (y < mapHeight - 1)
                        {
                            MapTile neighbor = compositeMap[x, y + 1];
                            if (!neighbor.isWall)
                            {
                                aStar.ConnectPoints(tile.pointId, neighbor.pointId);
                            }
                        }
                    }
                }
            }
        }

        private void SetWalls()
        {
            for (int i = 0; i < tileMaps.Count; i++)
            {
                TileMap map = tileMaps[i];
                if (map.Name.Contains("Wall"))
                {
                    Array<Vector2> usedCells = new Array<Vector2>(map.GetUsedCells());
                    for (int j = 0; j < usedCells.Count; j++)
                    {
                        int x = (int)usedCells[j].x;
                        int y = (int)usedCells[j].y;
                        MapTile tile = compositeMap[x, y];
                        tile.isWall = true;
                        tile.isBuildable = false;
                        aStar.SetPointDisabled(tile.pointId, true);
                    }
                }
                else if (map.Name.Contains("Path"))
                {
                    Array<Vector2> usedCells = new Array<Vector2>(map.GetUsedCells());
                    for (int j = 0; j < usedCells.Count; j++)
                    {
                        int x = (int)usedCells[j].x;
                        int y = (int)usedCells[j].y;
                        MapTile tile = compositeMap[x, y];
                        tile.isBuildable = false;
                    }
                }
            }
        }

        public MapTile GetMapTileByCoords(int x, int y)
        {
            if (compositeMap != null && x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
            {
                return compositeMap[x, y];
            }

            return null;
        }

        public MapTile GetMapTileByWorld(Vector2 worldPosition)
        {
            Vector2 movedPosition = worldPosition - Position;
            int x = Mathf.FloorToInt((movedPosition.x) / tileSize);
            int y = Mathf.FloorToInt((movedPosition.y) / tileSize);

            return GetMapTileByCoords(x, y);
        }

        public Vector2[] GetPath(Vector2 worldFrom, Vector2 worldTo)
        {
            MapTile source = GetMapTileByWorld(worldFrom);
            MapTile destination = GetMapTileByWorld(worldTo);

            Vector2[] nodeRoute = aStar.GetPointPath(source.pointId, destination.pointId);
            Vector2[] worldRoute = new Vector2[nodeRoute.Length];
            Vector2 offset = new Vector2(tileSize, tileSize) / 2.0f;
            for (int i = 0; i < nodeRoute.Length; i++)
            {
                worldRoute[i] = nodeRoute[i] * tileSize + offset;
            }

            return worldRoute;
        }

        public Vector2 GetTileSize()
        {
            return new Vector2(tileSize, tileSize);
        }

        private void ConstructCompositeMap()
        {
            compositeMap = new MapTile[mapWidth, mapHeight];

            int pointId = 0;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    compositeMap[x, y] = new MapTile(
                        this,
                        pointId,
                        x,
                        y,
                        new Vector2(x * tileSize, y * tileSize) + mapBounds.Position + Position
                    );
                    aStar.AddPoint(pointId, new Vector2(x, y));
                    pointId++;
                }
            }
        }

        private void CalculateMapBounds()
        {
            Vector2 totalTopLeft = new Vector2(Mathf.Inf, Mathf.Inf);
            Vector2 totalBotRight = new Vector2(-Mathf.Inf, -Mathf.Inf);

            for (int i = 0; i < tileMaps.Count; i++)
            {
                Rect2 usedRect = tileMaps[i].GetUsedRect();
                Vector2 topLeft = usedRect.Position;
                totalTopLeft.x = Mathf.Min(totalTopLeft.x, topLeft.x);
                totalTopLeft.y = Mathf.Min(totalTopLeft.y, topLeft.y);

                Vector2 botRight = usedRect.End;
                totalBotRight.x = Mathf.Max(totalBotRight.x, botRight.x);
                totalBotRight.y = Mathf.Max(totalBotRight.y, botRight.y);
            }

            mapBounds = new Rect2(totalTopLeft, (totalBotRight - totalTopLeft));
            mapWidth = Mathf.CeilToInt(mapBounds.Size.x);
            mapHeight = Mathf.CeilToInt(mapBounds.Size.y);
        }

        private void CollectTileMaps()
        {
            tileMaps = new List<TileMap>();
            for (int i = 0; i < GetChildCount(); i++)
            {
                if (GetChild(i) is TileMap tm)
                {
                    tileMaps.Add(tm);
                }
            }

            if (tileMaps.Count > 0)
            {
                tileSize = tileMaps[0].CellSize.x;
            }
        }
    }
}