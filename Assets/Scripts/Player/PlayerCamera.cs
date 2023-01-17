using Cinemachine;
using UnityEngine;

namespace TowerCreep.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera virtualCamera;

        private void Start()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void Teleport(Vector2 where)
        {
            virtualCamera.ForceCameraPosition(where, Quaternion.identity);
        }
    }
}