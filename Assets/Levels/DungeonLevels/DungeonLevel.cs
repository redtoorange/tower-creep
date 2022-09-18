using System;
using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using TowerCreep.Map.Doors;
using TowerCreep.Player;
using UnityEngine;

namespace TowerCreep.Levels.DungeonLevels
{
    public class DungeonLevel : MonoBehaviour
    {
        public Action OnDungeonLevelComplete;
        public Action OnPlayerExitedLevel;


        public static Action<DungeonLevel> OnPlayerEnteredLevel;

        [SerializeField] private EnemyController enemyController;
        [SerializeField] private DoorController doorController;
        [SerializeField] private PlayerSpawnPoint playerSpawnPoint;

        private void OnEnable()
        {
            doorController.OnPlayerHasEnteredRoom += HandlePlayerEnteredRoom;
            doorController.OnPlayerHasExitedRoom += HandlePlayerExitedRoom;

            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;
        }

        private void HandlePlayerExitedRoom()
        {
            doorController.LockAllDoors();
            OnPlayerExitedLevel?.Invoke();
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

        public void TeleportPlayer(PlayerController playerController, PlayerCamera playerCamera)
        {
            Vector3 position = playerSpawnPoint.transform.position;
            playerController.transform.position = position;
            playerCamera.Teleport(position);
        }
    }
}