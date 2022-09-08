using Godot;
using TowerCreep.Player;

namespace TowerCreep.Interface.ManaBar
{
    public class ManaDisplayController : Control
    {
        private Label manaLabel;
        private int currentMana = 0;

        public override void _EnterTree()
        {
            manaLabel = GetNode<Label>("Container/ManaLabel");
            PlayerResourceManager.OnResourceChange += HandlePlayerResourceChange;
        }

        public override void _ExitTree()
        {
            PlayerResourceManager.OnResourceChange -= HandlePlayerResourceChange;
        }

        private void HandlePlayerResourceChange(PlayerResourceType type, int oldValue, int newValue)
        {
            if (type == PlayerResourceType.Mana)
            {
                SetMana(newValue);
            }
        }

        public void SetMana(int newMana)
        {
            currentMana = newMana;
            manaLabel.Text = currentMana.ToString();
        }
    }
}