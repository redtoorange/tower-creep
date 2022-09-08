using System;
using Godot;
using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using TowerCreep.Map.Doors;
using TowerCreep.Towers.Placement;

namespace TowerCreep.Levels.DungeonLevels
{
    public class DungeonLevel : Node2D
    {
        public Action OnDungeonLevelComplete;

        public static Action<DungeonLevel> OnPlayerEnteredLevel;
        public static Action<DungeonLevel> OnPlayerExitedLevel;

        private EnemyController enemyController;
        private DoorController doorController;

        [Export] private bool debugStart = false;

        public override void _EnterTree()
        {
            doorController = GetNode<DoorController>("DoorController");
            doorController.OnPlayerHasEnteredRoom += HandlePlayerEnteredRoom;
            doorController.OnPlayerHasExitedRoom += HandlePlayerExitedRoom;

            enemyController = GetNode<EnemyController>("EnemyController");
            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;

            if (debugStart)
            {
                GD.PrintErr("Warning: Debug Start is enabled for ", Name);
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

        public override void _ExitTree()
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