using Godot;
using TowerCreep.Interface.Hotbar;
using TowerCreep.Interface.LevelProgress;
using TowerCreep.Interface.ManaBar;

namespace TowerCreep.Interface
{
    public class GameHudController : Node
    {
        [Export] private NodePath hotBarPath;
        [Export] private NodePath progressBarControllerPath;

        private HotbarController hotbarController;
        private ProgressBarController progressBarController;

        public override void _EnterTree()
        {
            hotbarController = GetNode<HotbarController>(hotBarPath);
            progressBarController = GetNode<ProgressBarController>(progressBarControllerPath);
        }
    }
}