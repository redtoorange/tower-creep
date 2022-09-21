using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Utility
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager S;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }

        public void ChangeToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        public void ChangeToTowerSelection()
        {
            SceneManager.LoadScene("TowerSelectionScreen");
        }
        
        public void ChangeToGame()
        {
            SceneManager.LoadScene("MainGame");
        }
        
        public void ChangeToWinScreen()
        {
            SceneManager.LoadScene("WinScreen");
        }
        
        public void ChangeToLoseScreen()
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}