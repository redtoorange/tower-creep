using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Towers.Selection
{
    public class TowerSelectionController : MonoBehaviour
    {
        public Action<Tower> OnTowerSelected;
        public Action<Tower> OnTowerDeSelected;

        [SerializeField] private ContactFilter2D towerFilter;

        private Camera camera;

        private Tower hoveredTower;
        private Tower selectedTower;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void OnEnable()
        {
            Tower.OnTowerSelectionStateChange += HandleTowerSelectionChange;
        }

        private void OnDisable()
        {
            Tower.OnTowerSelectionStateChange -= HandleTowerSelectionChange;
        }

        private void HandleTowerSelectionChange(TowerSelectionStateChange stateChange)
        {
            if (stateChange.newSelectedState != stateChange.oldSelectedState)
            {
                if (stateChange.newSelectedState == TowerSelectionState.Selected)
                {
                    OnTowerSelected?.Invoke(stateChange.tower);
                }
                else if (stateChange.newSelectedState == TowerSelectionState.DeSelected)
                {
                    OnTowerDeSelected?.Invoke(stateChange.tower);
                }
            }
        }

        public void ProcessUpdate()
        {
            List<Tower> towers = CheckForTower();
            if (towers.Count == 1)
            {
                hoveredTower = towers[0];
                hoveredTower.SetHovered(true);
            }
            else
            {
                if (!ReferenceEquals(hoveredTower, null))
                {
                    hoveredTower.SetHovered(false);
                    hoveredTower = null;
                }
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

        public void HandleLeftClick(InputAction.CallbackContext callbackContext)
        {
            if (!ReferenceEquals(hoveredTower, null))
            {
                if (!ReferenceEquals(selectedTower, null))
                {
                    selectedTower.SetSelected(false);
                }

                hoveredTower.SetSelected(true);
                selectedTower = hoveredTower;
            }
        }

        public void HandleRightClick(InputAction.CallbackContext callbackContext)
        {
            if (!ReferenceEquals(selectedTower, null))
            {
                selectedTower.SetSelected(false);
                selectedTower = null;
            }
        }
    }
}