using Godot;

public class PauseMenu : Control
{
    [Export] private PackedScene mainMenu;
    
    private bool isPaused;

    public override void _Ready()
    {
        Hide();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("PauseGame"))
        {
            if (isPaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        GetTree().Paused = true;
        Show();
    }

    public void UnPauseGame()
    {
        Hide();
        GetTree().Paused = false;
        isPaused = false;
    }

    public void OnResumePressed()
    {
        UnPauseGame();
    }

    public void OnSettingsPressed()
    {
        
    }

    public void OnMainMenuPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneTo(mainMenu);
    }
}