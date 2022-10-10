using System;
using UnityEngine;

namespace TowerCreep
{
    public class CircleDraw : MonoBehaviour
    {
        [SerializeField] private float smoothness = 0.01f;
        [SerializeField] private float radius = 3f;
        [SerializeField] private float lineThickness = 0.1f;

        private LineRenderer lineRenderer;
        private int pointCount;

        private void Start()
        {
            ConstructCircle();
        }

        private void Update()
        {
            ConstructCircle();
        }

        [ContextMenu("Do Something")]
        private void ConstructCircle()
        {
            pointCount = (int)(2.0f * Mathf.PI / smoothness) + 1;

            lineRenderer = GetComponent<LineRenderer>();
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
    }
}