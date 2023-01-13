using TMPro;
using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.Tooltip
{
    public class ToolTipDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text toolTipText;
        [SerializeField] private Vector2 offset;
        private ToolTipTarget currentTarget;
        private RectTransform rectTransform;

        public void SetPosition(Vector2 position)
        {
            if (ReferenceEquals(rectTransform, null))
            {
                rectTransform = GetComponent<RectTransform>();
            }

            rectTransform.localPosition = position + offset;
        }

        public void SetTarget(ToolTipTarget target)
        {
            currentTarget = target;
            toolTipText.text = currentTarget.GetText();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            currentTarget = null;
            gameObject.SetActive(false);
        }
    }
}