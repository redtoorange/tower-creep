using UnityEngine;

namespace TowerCreep.Towers.Placement
{
    public class BuildableTile : MonoBehaviour
    {
        public Vector2 centerOffset;
        public bool isOccupied;

        public void Initialize(Vector2 centerOffset)
        {
            this.centerOffset = centerOffset;
        }
    }
}