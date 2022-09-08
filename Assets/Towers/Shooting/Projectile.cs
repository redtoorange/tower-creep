using Godot;
using TowerCreep.Map.Doors;

namespace TowerCreep.Towers.Shooting
{
    public class Projectile : KinematicBody2D
    {
        // Exported Properties
        [Export] private float speed = 100.0f;
        [Export] private float damage = 1.0f;
        [Export] private bool keepOnMap = true;

        // External Nodes
        private CollisionShape2D collisionShape2D;
        private AudioStreamPlayer2D impactAudioPlayer;

        // Internal State
        private bool hasTarget = false;
        private Vector2 targetPoint;
        private Vector2 targetDirection;

        public override void _Ready()
        {
            collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
            impactAudioPlayer = GetNode<AudioStreamPlayer2D>("ImpactAudioPlayer");
            impactAudioPlayer.Connect("finished", this, nameof(OnAudioPlayerFinished));
        }

        public void FireAt(Enemy.Enemy enemy)
        {
            if (enemy != null)
            {
                targetPoint = enemy.GlobalPosition;
                targetDirection = (targetPoint - GlobalPosition).Normalized();
                Rotation = targetDirection.Angle();

                hasTarget = true;
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!hasTarget) return;

            MoveAndSlide(targetDirection * speed);
            int slideCount = GetSlideCount();
            for (int i = 0; i < slideCount; i++)
            {
                KinematicCollision2D collision = GetSlideCollision(i);
                if (collision != null)
                {
                    bool playSound = false;
                    if (collision.Collider is Enemy.Enemy e)
                    {
                        playSound = true;
                        e.TakeDamage(damage);
                        hasTarget = false;
                        collisionShape2D.Disabled = true;
                        Visible = false;
                        keepOnMap = false;
                    }
                    else if (collision.Collider is TileMap sb || collision.Collider is Door d)
                    {
                        playSound = true;
                        hasTarget = false;
                        collisionShape2D.Disabled = true;

                        if (!keepOnMap)
                        {
                            Visible = false;
                        }
                    }

                    if (playSound)
                    {
                        impactAudioPlayer.Play();
                    }
                }
            }
        }

        private void OnAudioPlayerFinished()
        {
            if (!keepOnMap)
            {
                QueueFree();
            }
        }
    }
}