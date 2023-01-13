using UnityEngine;

namespace TowerCreep.TowerCreep2D.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerSprite playerSprite;

        private Vector2 inputVector = Vector2.zero;
        private GameInputActions gameInputActions;
        private Rigidbody2D rigidbody2D;

        private void Start()
        {
            gameInputActions = new GameInputActions();
            gameInputActions.Enable();

            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void HandleInput()
        {
            Vector2 tempInput = gameInputActions.PlayerActions.Movement.ReadValue<Vector2>();
            inputVector = tempInput.normalized;
        }

        private void FixedUpdate()
        {
            HandleInput();
            rigidbody2D.MovePosition(rigidbody2D.position + inputVector * (speed * Time.fixedDeltaTime));
            playerSprite.UpdateAnimation(inputVector);
        }
    }
}