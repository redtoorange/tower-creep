using Godot;

namespace TowerCreep.Towers.Placement
{
    public class BuildableTile : Node2D
    {
        public Vector2 centerOffset;
        public bool isOccupied;

        public void Initialize(Vector2 centerOffset)
        {
            this.centerOffset = centerOffset;
        }
    }
}