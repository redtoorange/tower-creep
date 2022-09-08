using Godot;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public abstract class HoverableSlot : Control
    {
        protected bool isHovered = false;

        public override void _Input(InputEvent @event)
        {
            if (!isHovered) return;

            if (@event is InputEventMouseButton inputEventMouseButton)
            {
                if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Left && inputEventMouseButton.Doubleclick)
                {
                    HandleDoubleLeftClicked();
                }
                else if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Right && inputEventMouseButton.Pressed)
                {
                    HandleRightClicked();
                }
            }
        }

        public override void _Ready()
        {
            Connect("mouse_entered", this, nameof(HandleMouseEntered));
            Connect("mouse_exited", this, nameof(HandleMouseExited));
        }

        public void HandleMouseEntered()
        {
            isHovered = true;
        }

        public void HandleMouseExited()
        {
            isHovered = false;
        }

        protected abstract void HandleDoubleLeftClicked();
        protected abstract void HandleRightClicked();
    }
}