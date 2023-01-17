using System;
using System.Collections.Generic;
using TowerCreep.TowerCreep2D.Scripts.Map.DungeonLevels;
using TowerCreep.TowerCreep2D.Scripts.Player;
using TowerCreep.TowerCreep2D.Scripts.Utility;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Map
{
    public class LevelManager : MonoBehaviour
    {
        public static Action OnGameWin;
        public static Action OnNewLevelLoaded;

        [SerializeField] private List<DungeonLevel> levels;
        [SerializeField] private int currentLevelIndex = 0;

        private List<DungeonLevel> instancedLevels;

        [SerializeField] private Transform dungeonLevelContainer;
        private DungeonLevel currentLevel;
        private PlayerController playerController;
        private PlayerCamera playerCamera;

        private string winScreen = "WinScreen";
        private string loseScreen = "LoseScreen";

        private void Start()
        {
            instancedLevels = new List<DungeonLevel>(dungeonLevelContainer.GetComponentsInChildren<DungeonLevel>());

            // Automatically start the first level found
            if (instancedLevels.Count > 0)
            {
                currentLevel = instancedLevels[0];
                instancedLevels[instancedLevels.Count - 1].OnPlayerExitedLevel += HandleDungeonLevelComplete;
                currentLevelIndex = instancedLevels.Count - 1;
            }

            playerController = FindObjectOfType<PlayerController>();
            playerCamera = FindObjectOfType<PlayerCamera>();
        }

        public DungeonLevel LoadLevel(DungeonLevel level)
        {
            DungeonLevel newLevel = Instantiate(level, dungeonLevelContainer);
            newLevel.OnPlayerExitedLevel += HandleDungeonLevelComplete;
            instancedLevels.Add(newLevel);

            if (!ReferenceEquals(currentLevel, null))
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
            if (!ReferenceEquals(currentLevel, null))
            {
                TransitionController.S.FadeIn();
            }
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count)
            {
                LoadLevel(levels[currentLevelIndex]);
                OnNewLevelLoaded?.Invoke();
            }
            else
            {
                OnGameWin?.Invoke();
                GameManager.S.ChangeToWinScreen();
            }
        }
    }
}