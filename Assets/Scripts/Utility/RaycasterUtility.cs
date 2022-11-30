using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Utility
{
    public class RaycasterUtility : MonoBehaviour
    {
        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            Vector2 mPos = Mouse.current.position.ReadValue();
            Vector2 wPos = camera.ScreenToWorldPoint(mPos);

            RaycastHit2D[] hits = new RaycastHit2D[5];
            int count = Physics2D.RaycastNonAlloc(wPos, Vector2.zero, hits);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = hits[i];
                    Debug.Log("Hit-" + i + ": " + hit.collider.gameObject.name);
                }
            }
        }
    }
}