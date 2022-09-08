using UnityEngine;

namespace TowerCreep.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerSprite playerSprite;

        private Vector2 inputVector = Vector2.zero;
        private GameInputActions gameInputActions;

        private void Start()
        {
            gameInputActions = new GameInputActions();
            gameInputActions.Enable();
        }

        private void HandleInput()
        {
            Vector2 tempInput = gameInputActions.PlayerActions.Movement.ReadValue<Vector2>();
            inputVector = tempInput.normalized;
        }

        private void FixedUpdate()
        {
            HandleInput();
            // Vector2 result = MoveAndSlide(inputVector * speed);
            playerSprite.UpdateAnimation(inputVector);
        }
    }
}