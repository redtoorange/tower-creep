using Godot;
using TowerCreep.Player;

namespace TowerCreep.Interface.HealthDisplay
{
    public class HealthDisplayController : Control
    {
        private Label healthLabel;
        private int currentHealth = 0;

        public override void _EnterTree()
        {
            healthLabel = GetNode<Label>("HealthLabel");
            PlayerResourceManager.OnResourceChange += HandlePlayerResourceChange;
        }

        public override void _ExitTree()
        {
            PlayerResourceManager.OnResourceChange -= HandlePlayerResourceChange;
        }

        private void HandlePlayerResourceChange(PlayerResourceType type, int oldValue, int newValue)
        {
            if (type == PlayerResourceType.Health)
            {
                SetHealth(newValue);
            }
        }

        public void SetHealth(int newHealth)
        {
            currentHealth = newHealth;
            healthLabel.Text = currentHealth.ToString();
        }
    }
}