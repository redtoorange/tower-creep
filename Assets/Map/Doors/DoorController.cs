using System;
using System.Collections.Generic;
using Godot;
using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;

namespace TowerCreep.Map.Doors
{
    public class DoorController : Node2D
    {
        public Action OnPlayerHasEnteredRoom;
        public Action OnPlayerHasExitedRoom;

        [Export] private NodePath enemyControllerPath;

        private List<Door> controlledDoors;
        private EnemyController enemyController;

        private void HandleEnemyControllerEvent(EnemyControllerEvent ece)
        {
            if (ece.controller == enemyController && ece.type == EnemyControllerEventType.AllWavesComplete)
            {
                UnlockAllExits();
            }
        }

        public override void _Ready()
        {
            controlledDoors = new List<Door>();
            for (int i = 0; i < GetChildCount(); i++)
            {
                if (GetChild(i) is Door d)
                {
                    controlledDoors.Add(d);
                    if (d.GetDoorType() == Door.DoorType.ExitDoor)
                    {
                        d.OnPlayerHasCrossedDoor += HandlePlayerCrossedExitDoor;
                    }
                    else if (d.GetDoorType() == Door.DoorType.EntranceDoor)
                    {
                        d.OnPlayerHasCrossedDoor += HandlePlayerCrossedEntranceDoor;
                    }
                }
            }

            enemyController = GetNode<EnemyController>(enemyControllerPath);
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