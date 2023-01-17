using System;
using TowerCreep.TowerCreep2D.Scripts.Enemy;
using TowerCreep.TowerCreep2D.Scripts.Enemy.EnemyControllerEvents;
using TowerCreep.TowerCreep2D.Scripts.Interface.BuildPhase;
using TowerCreep.TowerCreep2D.Scripts.Map.Doors;
using TowerCreep.TowerCreep2D.Scripts.Player;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Map.DungeonLevels
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
            BuildPhaseController.OnBuildPhaseComplete += StartLevel;
        }

        private void OnDisable()
        {
            EnemyController.OnEnemyControllerEvent -= HandleEnemyControllerEvent;
            BuildPhaseController.OnBuildPhaseComplete -= StartLevel;
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