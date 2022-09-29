using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerCreep.Interface.Tooltip
{
    public class ToolTipManager : MonoBehaviour
    {
        [SerializeField] private float toolTipDelay = 0.5f;
        [SerializeField] private ToolTipDisplay toolTipDisplay;

        private List<ToolTipTarget> allTargets;
        private ToolTipTarget currentTarget;
        private float elapsedDelay;

        private RectTransform rectTransform;
        private Camera mainCamera;


        private void Start()
        {
            allTargets = new List<ToolTipTarget>(FindObjectsOfType<ToolTipTarget>(true));
            for (int i = 0; i < allTargets.Count; i++)
            {
                ToolTipTarget target = allTargets[i];
                target.OnMouseEnter += HandleOnMouseEnter;
                target.OnMouseExit += HandleOnMouseExit;
            }

            toolTipDisplay.Hide();

            rectTransform = GetComponent<RectTransform>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!ReferenceEquals(currentTarget, null))
            {
                if (elapsedDelay < toolTipDelay)
                {
                    elapsedDelay += Time.deltaTime;
                    if (elapsedDelay >= toolTipDelay)
                    {
                        toolTipDisplay.SetPosition(CalculateTooltipPosition());
                        toolTipDisplay.SetTarget(currentTarget);
                    }
                }
                else
                {
                    toolTipDisplay.SetPosition(CalculateTooltipPosition());
                }
            }
        }

        private Vector2 CalculateTooltipPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Mouse.current.position.ReadValue(),
                mainCamera,
                out Vector2 pos
            );
            return pos;
        }

        private void HandleOnMouseEnter(ToolTipTarget target)
        {
            if (target.ShouldDisplayToolTip())
            {
                currentTarget = target;
                elapsedDelay = 0.0f;
            }
        }

        private void HandleOnMouseExit(ToolTipTarget target)
        {
            toolTipDisplay.Hide();
            currentTarget = null;
        }
    }
}