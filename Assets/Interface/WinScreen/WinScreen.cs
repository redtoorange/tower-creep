using Godot;

public class WinScreen : Control
{
    [Export] private PackedScene mainMenuScene;
    
    public void OnMainMenuPressed()
    {
        GetTree().ChangeSceneTo(mainMenuScene);
    }
}