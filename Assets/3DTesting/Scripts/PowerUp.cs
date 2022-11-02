using UnityEngine;

namespace TowerCreep
{
    public class PowerUp : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] private float rotationSpeed = 60.0f;

        [Header("Bounce")]
        [SerializeField] private float movementSpeed = 2.0f;
        [SerializeField] private float maxYOffset = 1.0f;

        private Vector3 startingPosition;
        private bool movingUp = true;

        private void Start()
        {
            startingPosition = transform.position;
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
            ObjectBounce();
        }

        private void ObjectBounce()
        {
            if (movingUp)
            {
                transform.Translate(new Vector3(0, movementSpeed * Time.deltaTime, 0));
                if (transform.position.y - startingPosition.y >= maxYOffset)
                {
                    movingUp = false;
                }
            }
            else
            {
                transform.Translate(new Vector3(0, -movementSpeed * Time.deltaTime, 0));
                if (transform.position.y - startingPosition.y <= 0)
                {
                    movingUp = true;
                }
            }
        }
    }
}