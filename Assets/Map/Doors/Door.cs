using System;
using Godot;
using TowerCreep.Player;

namespace TowerCreep.Map.Doors
{
    public class Door : StaticBody2D
    {
        public Action<Door> OnPlayerHasCrossedDoor;

        [Serializable]
        public enum DoorType
        {
            EntranceDoor,
            ExitDoor
        }

        [Serializable]
        public enum DoorState
        {
            Locked,
            Unlocked,
            Open
        }

        [Export] private DoorState currentState = DoorState.Locked;
        [Export] private DoorType doorType = DoorType.EntranceDoor;
        [Export] private bool doNotUnlock = false;

        private Sprite openDoorSprite;
        private Sprite closedDoorSprite;
        private Area2D doorCrossedArea;

        private uint initialCollisionLayers;
        private uint initialCollisionMask;

        public override void _Ready()
        {
            openDoorSprite = GetNode<Sprite>("OpenDoorSprite");
            closedDoorSprite = GetNode<Sprite>("ClosedDoorSprite");

            doorCrossedArea = GetNode<Area2D>("DoorCrossedArea");
            doorCrossedArea.Connect("body_exited", this, nameof(HandleBodyExited));

            if (doorType == DoorType.EntranceDoor && !doNotUnlock)
            {
                UnlockDoor();
            }
        }

        public DoorType GetDoorType() => doorType;

        public void UnlockDoor()
        {
            if (currentState == DoorState.Locked)
            {
                currentState = DoorState.Unlocked;
                openDoorSprite.Visible = true;
                closedDoorSprite.Visible = false;

                DisableCollisions();
            }
        }

        public void LockDoor()
        {
            if (currentState != DoorState.Locked)
            {
                currentState = DoorState.Locked;
                openDoorSprite.Visible = false;
                closedDoorSprite.Visible = true;

                EnableCollisions();
            }
        }

        private void DisableCollisions()
        {
            initialCollisionLayers = CollisionLayer;
            CollisionLayer = 0;
            initialCollisionMask = CollisionMask;
            CollisionMask = 0;
        }

        private void EnableCollisions()
        {
            CollisionLayer = initialCollisionLayers;
            CollisionMask = initialCollisionMask;
        }

        private void HandleBodyExited(Node body)
        {
            if (body is PlayerController pc)
            {
                if (pc.GlobalPosition.y < GlobalPosition.y)
                {
                    OnPlayerHasCrossedDoor?.Invoke(this);
                }
            }
        }
    }
}