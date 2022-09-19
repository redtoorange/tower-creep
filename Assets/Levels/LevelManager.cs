using System;
using System.Collections.Generic;
using TowerCreep.Levels.DungeonLevels;
using TowerCreep.Player;
using UnityEngine;

namespace TowerCreep.Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static Action OnGameWin;
        [SerializeField] private List<DungeonLevel> levels;
        [SerializeField] private int currentLevelIndex = 0;

        private List<DungeonLevel> instancedLevels;

        [SerializeField] private Transform dungeonLevelContainer;
        private DungeonLevel currentLevel;
        private PlayerController playerController;
        private PlayerCamera playerCamera;

        private void Start()
        {
            instancedLevels = new List<DungeonLevel>(dungeonLevelContainer.GetComponentsInChildren<DungeonLevel>());

            // Automatically start the first level found
            if (instancedLevels.Count > 0)
            {
                currentLevel = instancedLevels[0];
                instancedLevels[0].StartLevel();
                instancedLevels[instancedLevels.Count - 1].OnPlayerExitedLevel += HandleDungeonLevelComplete;
                currentLevelIndex = instancedLevels.Count - 1;
            }
            else
            {
                LoadLevel(levels[currentLevelIndex]).StartLevel();
            }

            playerController = FindObjectOfType<PlayerController>();
            playerCamera = FindObjectOfType<PlayerCamera>();
        }

        public DungeonLevel LoadLevel(DungeonLevel level)
        {
            DungeonLevel newLevel = Instantiate(level, dungeonLevelContainer);
            newLevel.OnPlayerExitedLevel += HandleDungeonLevelComplete;
            instancedLevels.Add(newLevel);

            if (currentLevel != null)
            {
                Destroy(currentLevel);
                instancedLevels.Remove(currentLevel);
                currentLevel.OnPlayerExitedLevel -= HandleDungeonLevelComplete;
                currentLevel.gameObject.SetActive(false);
            }

            currentLevel = newLevel;
            currentLevel.TeleportPlayer(playerController, playerCamera);
            return newLevel;
        }

        private void HandleDungeonLevelComplete()
        {
            TransitionController.S.FadeOut(FadeOutComplete);
        }

        private void FadeOutComplete()
        {
            LoadNextLevel();
            if (currentLevel != null)
            {
                TransitionController.S.FadeIn();
                currentLevel.StartLevel();
            }
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count)
            {
                LoadLevel(levels[currentLevelIndex]);
            }
            else
            {
                OnGameWin?.Invoke();
            }
        }
    }
}