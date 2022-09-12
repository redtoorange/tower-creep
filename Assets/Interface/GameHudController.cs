using TowerCreep.Interface.Hotbar;
using TowerCreep.Interface.LevelProgress;
using UnityEngine;

namespace TowerCreep.Interface
{
    public class GameHudController : MonoBehaviour
    {
        [SerializeField] private HotbarController hotbarController;
        [SerializeField] private ProgressBarController progressBarController;
    }
}