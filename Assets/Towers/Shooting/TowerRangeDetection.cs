using System;
using Godot;

namespace TowerCreep.Towers.Shooting
{
    public class TowerRangeDetection : Area2D
    {
        public Action<Enemy.Enemy> OnEnemyHasEnteredRange;
        public Action<Enemy.Enemy> OnEnemyHasExitedRange;

        public override void _Ready()
        {
            Connect("body_entered", this, nameof(HandleOnBodyEntered));
            Connect("body_exited", this, nameof(HandleOnBodyExited));
        }

        private void HandleOnBodyEntered(Node other)
        {
            if (other is Enemy.Enemy e)
            {
                OnEnemyHasEnteredRange?.Invoke(e);
            }
        }

        private void HandleOnBodyExited(Node other)
        {
            if (other is Enemy.Enemy e)
            {
                OnEnemyHasExitedRange?.Invoke(e);
            }
        }
    }
}