using System;
using System.Collections.Generic;
using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using UnityEngine;

namespace TowerCreep.Map.Doors
{
    public class DoorController : MonoBehaviour
    {
        public Action OnPlayerHasEnteredRoom;
        public Action OnPlayerHasExitedRoom;

        [SerializeField] private List<Door> controlledDoors;
        [SerializeField] private EnemyController enemyController;

        private void HandleEnemyControllerEvent(EnemyControllerEvent ece)
        {
            if (ece.controller == enemyController && ece.type == EnemyControllerEventType.AllWavesComplete)
            {
                Debug.Log("Unlocking all doors");
                UnlockAllExits();
            }
        }

        private void Start()
        {
            controlledDoors = new List<Door>(GetComponentsInChildren<Door>());
            for (int i = 0; i < controlledDoors.Count; i++)
            {
                Door d = controlledDoors[i];
                if (d.GetDoorType() == Door.DoorType.ExitDoor)
                {
                    d.OnPlayerHasCrossedDoor += HandlePlayerCrossedExitDoor;
                }
                else if (d.GetDoorType() == Door.DoorType.EntranceDoor)
                {
                    d.OnPlayerHasCrossedDoor += HandlePlayerCrossedEntranceDoor;
                }
            }

            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;
        }

        // This indicates the player fully entered the room
        private void HandlePlayerCrossedEntranceDoor(Door door)
        {
            OnPlayerHasEnteredRoom?.Invoke();
        }

        // This indicates the player fully left a room
        private void HandlePlayerCrossedExitDoor(Door door)
        {
            OnPlayerHasExitedRoom?.Invoke();
        }

        public void UnlockAllExits()
        {
            foreach (Door door in controlledDoors)
            {
                if (door.GetDoorType() == Door.DoorType.ExitDoor)
                {
                    door.UnlockDoor();
                }
            }
        }

        public void UnlockEntrance()
        {
            foreach (Door door in controlledDoors)
            {
                if (door.GetDoorType() == Door.DoorType.EntranceDoor)
                {
                    door.UnlockDoor();
                }
            }
        }

        public void LockAllDoors()
        {
            foreach (Door door in controlledDoors)
            {
                door.LockDoor();
            }
        }
    }
}