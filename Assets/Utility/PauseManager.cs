using System;
using UnityEngine;

namespace TowerCreep.Utility
{
    public class PauseManager : MonoBehaviour
    {
        public Action OnGamePaused;
        public Action OnGameUnpaused;
        public static PauseManager S;
        [SerializeField] private bool isGamePaused;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
            }
            else
            {
                Debug.LogError("Error: Two copies of a singleton in the scene, destroying the newest one");
                enabled = false;
                Destroy(this);
            }
        }

        private void Start()
        {
            if (isGamePaused)
            {
                isGamePaused = true;
                Time.timeScale = 0.0f;
                OnGamePaused?.Invoke();
            }
            else
            {
                isGamePaused = false;
                OnGameUnpaused?.Invoke();
                Time.timeScale = 1.0f;
            }
        }

        public bool IsGamePaused()
        {
            return isGamePaused;
        }

        public void SetGamePaused(bool paused)
        {
            if (paused == isGamePaused) return;

            if (paused)
            {
                isGamePaused = true;
                Time.timeScale = 0.0f;
                OnGamePaused?.Invoke();
            }
            else
            {
                isGamePaused = false;
                OnGameUnpaused?.Invoke();
                Time.timeScale = 1.0f;
            }
        }
    }
}