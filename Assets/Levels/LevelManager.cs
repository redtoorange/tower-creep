using System;
using System.Collections.Generic;

using TowerCreep.Levels.DungeonLevels;
using UnityEngine;

namespace TowerCreep.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static Action OnGameWin;
        [SerializeField] private List<DungeonLevel> levels;
        [SerializeField] private int currentLevelIndex = 0;

        private List<DungeonLevel> instancedLevels;

        private void Start()
        {
            
            instancedLevels = new List<DungeonLevel>(GetComponentsInChildren<DungeonLevel>());

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

        public DungeonLevel LoadLevel(DungeonLevel level)
        {
            DungeonLevel newLevel = Instantiate(level, transform);
            newLevel.OnDungeonLevelComplete += HandleDungeonLevelComplete;
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