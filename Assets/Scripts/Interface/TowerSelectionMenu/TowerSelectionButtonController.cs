using System;
using TowerCreep.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace TowerCreep.Interface.TowerSelectionMenu
{
    public class TowerSelectionButtonController : MonoBehaviour
    {
        public Action OnBackPressed;
        public Action OnRandomPressed;
        public Action OnReadyPressed;
        public Action OnResetPressed;

        [SerializeField] private Button readyButton;
        [SerializeField] private Button resetButton;

        public void SetReadyEnabled(bool isEnabled)
        {
            readyButton.interactable = isEnabled;
        }

        public void SetResetEnabled(bool isEnabled)
        {
            resetButton.interactable = isEnabled;
        }

        public void HandleBackButtonPressed()
        {
            OnBackPressed?.Invoke();
            GameManager.S.ChangeToMainMenu();
        }

        public void HandleRandomButtonPressed()
        {
            OnRandomPressed?.Invoke();
        }

        public void HandleReadyButtonPressed()
        {
            OnReadyPressed?.Invoke();
        }

        public void HandleResetButtonPressed()
        {
            OnResetPressed?.Invoke();
        }
    }
}