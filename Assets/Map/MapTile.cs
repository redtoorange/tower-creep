using Godot;

namespace TowerCreep.Map
{
    public class MapTile
    {
        public int compositePositionX;
        public int compositePositionY;
        public Vector2 worldPosition;
        public bool isOccupied = false;
        public bool isWall = false;
        public bool isBuildable = true;
        public int pointId;

        private MapController mapController;

        public MapTile(MapController mapController, int pointId, int compositePositionX, int compositePositionY,
            Vector2 worldPosition)
        {
            this.mapController = mapController;
            this.pointId = pointId;
            this.compositePositionX = compositePositionX;
            this.compositePositionY = compositePositionY;
            this.worldPosition = worldPosition;
        }

        public void SetOccupied(bool isOccupied)
        {
            mapController.SetTileOccupied(this, isOccupied);
        }
    }
}