using System.Collections.Generic;
using TowerCreep.Towers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Utility
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager S;
        private List<TowerData> selectedPlayerParty;

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

        public void SetPlayerParty(List<TowerData> selectedPlayerParty)
        {
            this.selectedPlayerParty = selectedPlayerParty;
        }

        public List<TowerData> GetSelectedPlayerParty()
        {
            return selectedPlayerParty;
        }
    }
}