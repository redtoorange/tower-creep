using System;

using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using TowerCreep.Map.Doors;
using TowerCreep.Towers.Placement;
using UnityEngine;

namespace TowerCreep.Levels.DungeonLevels
{
    public class DungeonLevel : MonoBehaviour
    {
        public Action OnDungeonLevelComplete;

        public static Action<DungeonLevel> OnPlayerEnteredLevel;
        public static Action<DungeonLevel> OnPlayerExitedLevel;

        [SerializeField] private EnemyController enemyController;
        [SerializeField] private DoorController doorController;

        [SerializeField] private bool debugStart = false;

        private void OnEnable()
        {
            doorController.OnPlayerHasEnteredRoom += HandlePlayerEnteredRoom;
            doorController.OnPlayerHasExitedRoom += HandlePlayerExitedRoom;

            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;

            if (debugStart)
            {
                Debug.LogError("Warning: Debug Start is enabled for ");
                StartLevel();
            }
        }

        private void HandlePlayerExitedRoom()
        {
            doorController.LockAllDoors();
            OnPlayerExitedLevel?.Invoke(this);
        }

        private void HandlePlayerEnteredRoom()
        {
            doorController.LockAllDoors();
            OnPlayerEnteredLevel?.Invoke(this);
            StartLevel();
        }

        private void OnDisable()
        {
            EnemyController.OnEnemyControllerEvent -= HandleEnemyControllerEvent;
        }

        private void HandleEnemyControllerEvent(EnemyControllerEvent ece)
        {
            if (ece.controller == enemyController && ece.type == EnemyControllerEventType.AllWavesComplete)
            {
                OnDungeonLevelComplete?.Invoke();
            }
        }

        public void StartLevel()
        {
            enemyController.StartSpawningMonsters();
        }
    }
}