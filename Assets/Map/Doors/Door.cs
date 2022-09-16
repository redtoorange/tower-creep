using System;

using TowerCreep.Player;
using UnityEngine;

namespace TowerCreep.Map.Doors
{
    public class Door : MonoBehaviour
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

        [SerializeField] private DoorState currentState = DoorState.Locked;
        [SerializeField] private DoorType doorType = DoorType.EntranceDoor;
        [SerializeField] private bool doNotUnlock = false;

        [SerializeField] private SpriteRenderer openDoorSprite;
        [SerializeField] private SpriteRenderer closedDoorSprite;
        [SerializeField] private BoxCollider2D doorCollider;

        private void Start()
        {
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
                openDoorSprite.enabled = true;
                closedDoorSprite.enabled = false;

                DisableCollisions();
            }
        }

        public void LockDoor()
        {
            if (currentState != DoorState.Locked)
            {
                currentState = DoorState.Locked;
                openDoorSprite.enabled = false;
                closedDoorSprite.enabled = true;

                EnableCollisions();
            }
        }

        private void DisableCollisions()
        {
            doorCollider.isTrigger = true;
        }

        private void EnableCollisions()
        {
            doorCollider.isTrigger = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController pc))
            {
                if (pc.transform.position.y > transform.position.y)
                {
                    OnPlayerHasCrossedDoor?.Invoke(this);
                }
            }
        }
    }
}