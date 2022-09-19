using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Interface.WinScreen
{
    public class WinScreenController : MonoBehaviour
    {
        public void OnMainMenuPressed()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnPlayAgainPressed()
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}