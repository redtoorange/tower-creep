using System.Collections.Generic;
using UnityEngine;

namespace TowerCreep.Interface.Tooltip
{
    public class ToolTipManager : MonoBehaviour
    {
        [SerializeField] private float toolTipDelay = 0.5f;
        [SerializeField] private ToolTipDisplay toolTipDisplay;

        private List<ToolTipTarget> allTargets;
        private ToolTipTarget currentTarget;
        private float elapsedDelay;


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
        }

        private void Update()
        {
            if (!ReferenceEquals(currentTarget, null) && elapsedDelay < toolTipDelay)
            {
                elapsedDelay += Time.deltaTime;
                if (elapsedDelay >= toolTipDelay)
                {
                    toolTipDisplay.SetTarget(currentTarget);
                }
            }
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