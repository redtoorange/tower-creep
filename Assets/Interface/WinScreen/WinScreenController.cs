using TowerCreep.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerCreep.Interface.WinScreen
{
    public class WinScreenController : MonoBehaviour
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