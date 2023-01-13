using System;
using TowerCreep.TowerCreep2D.Scripts.Player;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Map.Doors
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

        [SerializeField] private GameObject openDoorState;
        [SerializeField] private GameObject closedDoorState;
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
                openDoorState.SetActive(true);
                closedDoorState.SetActive(false);

                DisableCollisions();
            }
        }

        public void LockDoor()
        {
            if (currentState != DoorState.Locked)
            {
                currentState = DoorState.Locked;
                openDoorState.SetActive(false);
                closedDoorState.SetActive(true);

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