using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Interface.Menu
{
    /// <summary>
    /// Main menu for the game, will be the first screen we see.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string newGameScene;
        [SerializeField] private string loadGameScene;
        [SerializeField] private string settingsScene;

        public void OnNewGamePressed()
        {
            SceneManager.LoadScene(newGameScene);
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