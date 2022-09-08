using Godot;
using TowerCreep.Levels;
using TowerCreep.Player;

public class MainGame : Node2D
{
    [Export] private PackedScene winScreen;
    [Export] private PackedScene loseScreen;

    public override void _Ready()
    {
        LevelManager.OnGameWin += HandleOnWin;
        PlayerResourceManager.OnPlayerDie += HandleOnLose;
    }

    public override void _ExitTree()
    {
        LevelManager.OnGameWin -= HandleOnWin;
        PlayerResourceManager.OnPlayerDie -= HandleOnLose;
    }

    private void HandleOnWin()
    {
        QueueFree();
        GetTree().ChangeSceneTo(winScreen);
    }

    private void HandleOnLose()
    {
        QueueFree();
        GetTree().ChangeSceneTo(loseScreen);
    }
}