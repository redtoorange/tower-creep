using TowerCreep.TowerCreep2D.Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.Menu
{
    /// <summary>
    /// Main menu for the game, will be the first screen we see.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string newGameScene;
        [SerializeField] private string loadGameScene;
        [SerializeField] private string settingsScene;

        public void OnNewGamePressed()
        {
            GameManager.S.ChangeToTowerSelection();
        }

        public void OnLoadGamePressed()
        {
            SceneManager.LoadScene(loadGameScene);
        }

        public void OnSettingsPressed()
        {
            SceneManager.LoadScene(settingsScene);
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}