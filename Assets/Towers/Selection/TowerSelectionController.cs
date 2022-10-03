using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Towers.Selection
{
    public class TowerSelectionController : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D towerFilter;

        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        public void ProcessUpdate()
        {
            List<Tower> towers = CheckForTower();
            if (towers.Count == 1)
            {
                Debug.Log("Hovering Over " + towers[0].gameObject.name);
            }
        }

        private List<Tower> CheckForTower()
        {
            Vector2 mPos = Mouse.current.position.ReadValue();
            Vector2 wPos = camera.ScreenToWorldPoint(mPos);

            List<RaycastHit2D> hitResults = new List<RaycastHit2D>();
            Physics2D.Raycast(wPos, Vector2.zero, towerFilter, hitResults);

            List<Tower> towers = new List<Tower>();
            if (hitResults.Count > 0)
            {
                for (int i = 0; i < hitResults.Count; i++)
                {
                    if (hitResults[i].collider.TryGetComponent(out Tower t))
                    {
                        towers.Add(t);
                    }
                }
            }

            return towers;
        }
    }
}