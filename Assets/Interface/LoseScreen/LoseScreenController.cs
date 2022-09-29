using TowerCreep.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Interface.LoseScreen
{
    public class LoseScreenController : MonoBehaviour
    {
        public void OnMainMenuPressed()
        {
            GameManager.S.ChangeToMainMenu();
        }

        public void OnPlayAgainPressed()
        {
            GameManager.S.ChangeToGame();
        }
    }
}