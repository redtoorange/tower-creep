using TMPro;
using UnityEngine;

namespace TowerCreep.Interface.Tooltip
{
    public class ToolTipDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text toolTipText;
        [SerializeField] private Vector2 offset;
        private ToolTipTarget currentTarget;

        public void SetTarget(ToolTipTarget target)
        {
            currentTarget = target;
            toolTipText.text = currentTarget.GetText();
            transform.position = target.GetPosition() + offset;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            currentTarget = null;
            gameObject.SetActive(false);
        }
    }
}