using Godot;

namespace TowerCreep.Interface.Menu
{
    /// <summary>
    /// Main menu for the game, will be the first screen we see.
    /// </summary>
    public class MainMenu : Control
    {
        [Export(PropertyHint.ResourceType, "PackedScene")] private PackedScene newGameScene;
        [Export(PropertyHint.ResourceType, "PackedScene")] private PackedScene loadGameScene;
        [Export(PropertyHint.ResourceType, "PackedScene")] private PackedScene settingsScene;

        public void OnNewGamePressed()
        {
            GetTree().ChangeSceneTo(newGameScene);
        }

        public void OnLoadGamePressed()
        {
            GetTree().ChangeSceneTo(loadGameScene);
        }

        public void OnSettingsPressed()
        {
            GetTree().ChangeSceneTo(settingsScene);
        }

        public void OnQuitPressed()
        {
            GetTree().Quit();
        }
    }
}