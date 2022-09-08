using Godot;
using TowerCreep.Levels.DungeonLevels;

namespace TowerCreep.Levels.DungeonCorridors
{
    public class DungeonCorridor : Node2D
    {
        private DungeonLevel northLevel;
        private DungeonLevel southLevel;

        public void Initialize(
            DungeonLevel northLevel,
            DungeonLevel southLevel
        )
        {
            this.northLevel = northLevel;
            this.southLevel = southLevel;
        }
    }
}