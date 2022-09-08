using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Levels.DungeonLevels;

namespace TowerCreep.Levels
{
    public class LevelManager : Node
    {
        public static Action OnGameWin;
        [Export] private List<PackedScene> levels;
        [Export] private int currentLevelIndex = 0;

        private List<DungeonLevel> instancedLevels;

        public override void _Ready()
        {
            instancedLevels = new List<DungeonLevel>();
            for (int i = 0; i < GetChildCount(); i++)
            {
                if (GetChild(i) is DungeonLevel dl)
                {
                    instancedLevels.Add(dl);
                }
            }

            // Automatically start the first level found
            if (instancedLevels.Count > 0)
            {
                instancedLevels[0].StartLevel();
                instancedLevels[instancedLevels.Count - 1].OnDungeonLevelComplete += HandleDungeonLevelComplete;
                currentLevelIndex = instancedLevels.Count - 1;
            }
            else
            {
                LoadLevel(levels[currentLevelIndex]).StartLevel();
            }
        }

        public DungeonLevel LoadLevel(PackedScene level)
        {
            DungeonLevel newLevel = level.Instance<DungeonLevel>();
            newLevel.OnDungeonLevelComplete += HandleDungeonLevelComplete;
            AddChild(newLevel);
            instancedLevels.Add(newLevel);
            return newLevel;
        }

        private void HandleDungeonLevelComplete()
        {
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count)
            {
                LoadLevel(levels[currentLevelIndex]).StartLevel();
            }
            else
            {
                OnGameWin?.Invoke();
            }
        }
    }
}