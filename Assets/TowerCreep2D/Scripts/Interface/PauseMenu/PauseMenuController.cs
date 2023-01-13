using TowerCreep.TowerCreep2D.Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TowerCreep.TowerCreep2D.Scripts.Interface.PauseMenu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuContainer;
        private GameInputActions inputActions;

        private void Start()
        {
            PauseManager.S.OnGamePaused += ShowPauseMenu;
            PauseManager.S.OnGameUnpaused += HidePauseMenu;

            inputActions = new GameInputActions();
            inputActions.PlayerActions.PauseGame.performed += HandlePauseAction;
            inputActions.Enable();

            if (PauseManager.S.IsGamePaused())
            {
                ShowPauseMenu();
            }
            else
            {
                HidePauseMenu();
            }
        }

        private void OnDestroy()
        {
            PauseManager.S.OnGamePaused -= ShowPauseMenu;
            PauseManager.S.OnGameUnpaused -= HidePauseMenu;
            inputActions.PlayerActions.PauseGame.performed -= HandlePauseAction;
            inputActions.Disable();
        }

        private void HandlePauseAction(InputAction.CallbackContext obj)
        {
            if (PauseManager.S.IsGamePaused())
            {
                PauseManager.S.SetGamePaused(false);
            }
            else
            {
                PauseManager.S.SetGamePaused(true);
            }
        }

        private void HidePauseMenu()
        {
            menuContainer.SetActive(false);
        }

        private void ShowPauseMenu()
        {
            menuContainer.SetActive(true);
        }

        public void OnResumeClicked()
        {
            PauseManager.S.SetGamePaused(false);
        }

        public void OnRestartClicked()
        {
            SceneManager.LoadScene("MainGame");
        }

        public void OnSettingsClicked()
        {
            Debug.Log("Show Settings");
        }

        public void OnQuitClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}