using Godot;

namespace TowerCreep.Player
{
    /// <summary>
    /// Handles player movement
    /// </summary>
    public class PlayerController : KinematicBody2D
    {
        [Export] private float speed;

        private Vector2 inputVector = Vector2.Zero;
        private PlayerSprite playerSprite;

        public override void _Ready()
        {
            playerSprite = GetNode<PlayerSprite>("PlayerSprite");
        }

        private void HandleInput()
        {
            Vector2 tempInput = Vector2.Zero;
            if (Input.IsActionPressed("MoveUp"))
            {
                tempInput.y -= 1;
            }

            if (Input.IsActionPressed("MoveDown"))
            {
                tempInput.y += 1;
            }

            if (Input.IsActionPressed("MoveLeft"))
            {
                tempInput.x -= 1;
            }

            if (Input.IsActionPressed("MoveRight"))
            {
                tempInput.x += 1;
            }

            inputVector = tempInput.Normalized();
        }

        public override void _PhysicsProcess(float delta)
        {
            HandleInput();
            Vector2 result = MoveAndSlide(inputVector * speed);
            playerSprite.UpdateAnimation(result);
        }
    }
}