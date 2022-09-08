using Godot;

public class LoseScreen : Control
{
    [Export] private PackedScene mainMenuScene;

    public void OnMainMenuPressed()
    {
        GetTree().ChangeSceneTo(mainMenuScene);
    }
}