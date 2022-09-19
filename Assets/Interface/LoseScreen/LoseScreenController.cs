using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Interface.LoseScreen
{
    public class LoseScreenController : MonoBehaviour
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