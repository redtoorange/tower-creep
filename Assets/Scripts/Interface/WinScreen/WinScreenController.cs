using TowerCreep.Utility;
using UnityEngine;

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