using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Towers.Shooting
{
    [RequireComponent(typeof(LineRenderer))]
    public class TowerRangePreview : MonoBehaviour
    {
        [SerializeField] private float smoothness = 0.01f;
        [SerializeField] private float radius = 3.0f;
        [SerializeField] private float lineThickness = 0.1f;

        [SerializeField] private LineRenderer lineRenderer;
        private int pointCount;

        private void Awake()
        {
            SetShow(false);
        }

        private void Start()
        {
            Tower tower = GetComponentInParent<Tower>();
            if (!ReferenceEquals(tower, null))
            {
                tower.OnTowerSelected += HandleSelected;
                tower.OnTowerDeselected += HandleDeselected;
            }
        }

        private void HandleDeselected()
        {
            SetShow(false);
        }

        private void HandleSelected()
        {
            SetShow(true);
        }

        public void SetRange(float radius)
        {
            // Avoid updating the circle for very tiny changes
            if (Mathf.Epsilon < Mathf.Abs(this.radius - radius))
            {
                this.radius = radius;
                UpdateCircle();
            }
        }

        [ContextMenu("UpdateCircle()")]
        public void UpdateCircle()
        {
            pointCount = (int)(2.0f * Mathf.PI / smoothness) + 1;

            lineRenderer.startWidth = lineThickness;
            lineRenderer.endWidth = lineThickness;
            lineRenderer.positionCount = pointCount;
            lineRenderer.textureScale = new Vector2(radius / 10.0f, 1.0f);

            Vector3 transformPosition = transform.position;
            float theta = 0.0f;

            for (int i = 0; i < pointCount; i++)
            {
                theta += (2.0f * Mathf.PI * smoothness);
                float x = radius * Mathf.Cos(theta) + transformPosition.x;
                float y = radius * Mathf.Sin(theta) + transformPosition.y;
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        public void SetShow(bool shouldShow)
        {
            if (shouldShow)
            {
                UpdateCircle();
            }

            lineRenderer.enabled = shouldShow;
        }
    }
}