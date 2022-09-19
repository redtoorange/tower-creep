
using TowerCreep.Levels;
using TowerCreep.Player;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // [SerializeField] private PackedScene winScreen;
    // [SerializeField] private PackedScene loseScreen;

    private void Start()
    {
        LevelManager.OnGameWin += HandleOnWin;
        PlayerResourceManager.OnPlayerDie += HandleOnLose;
    }

    private void OnDisable()
    {
        LevelManager.OnGameWin -= HandleOnWin;
        PlayerResourceManager.OnPlayerDie -= HandleOnLose;
    }

    private void HandleOnWin()
    {
  
    }

    private void HandleOnLose()
    {

    }
}