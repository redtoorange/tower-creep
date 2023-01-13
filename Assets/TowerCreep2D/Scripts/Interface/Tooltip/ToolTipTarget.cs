using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.Tooltip
{
    public class ToolTipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Action<ToolTipTarget> OnMouseEnter;
        public Action<ToolTipTarget> OnMouseExit;

        [SerializeField] private string toolTip;
        [SerializeField] private Vector2 targetOffset;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExit?.Invoke(this);
        }

        public virtual bool ShouldDisplayToolTip()
        {
            return true;
        }

        public virtual string GetText()
        {
            return toolTip;
        }

        public virtual Vector2 GetPosition()
        {
            Vector2 pos = transform.position;
            return pos + targetOffset;
        }
    }
}