using UnityEngine;

namespace TowerCreep
{
    [RequireComponent(typeof(LineRenderer))]
    public class TowerRangePreview : MonoBehaviour
    {
        [SerializeField] private float smoothness = 0.01f;
        [SerializeField] private float radius = 3.0f;
        [SerializeField] private float lineThickness = 0.1f;

        private LineRenderer lineRenderer;
        private int pointCount;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            ConstructCircle();
            SetShow(false);
        }

        public void SetRange(float radius)
        {
            // Avoid updating the circle for very tiny changes
            if (Mathf.Epsilon > Mathf.Abs(this.radius - radius))
            {
                this.radius = radius;
                ConstructCircle();
            }
        }

        [ContextMenu("ConstructCircle()")]
        private void ConstructCircle()
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
            lineRenderer.enabled = shouldShow;
        }
    }
}