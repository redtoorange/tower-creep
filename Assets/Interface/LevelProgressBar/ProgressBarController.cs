using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using UnityEngine;
using UnityEngine.UI;


namespace TowerCreep.Interface.LevelProgressBar
{
    public class ProgressBarController : MonoBehaviour
    {
        private enum ProgressBarState
        {
            None,
            Wave,
            Cooldown
        }

        private ProgressBarState currentState = ProgressBarState.None;

        [SerializeField] private Slider waveProgressBar;
        [SerializeField] private Slider cooldownProgressBar;

        private void Start()
        {
            HideBars();
        }

        private void OnEnable()
        {
            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;
        }

        private void OnDisable()
        {
            EnemyController.OnEnemyControllerEvent -= HandleEnemyControllerEvent;
        }

        private void HandleEnemyControllerEvent(EnemyControllerEvent ece)
        {
            if (ece is EnemyControlledTimedEvent timedEvent)
            {
                if (timedEvent.type == EnemyControllerEventType.CooldownStarted)
                {
                    if (!IsCooldownRunning())
                    {
                        StartCooldown(timedEvent.length);
                    }
                }
                else if (timedEvent.type == EnemyControllerEventType.WaveStart)
                {
                    if (!IsWaveRunning())
                    {
                        StartWave(timedEvent.length);
                    }
                }
            }
            else if (ece.type == EnemyControllerEventType.AllWavesComplete)
            {
                HideBars();
            }
        }

        private void StartWave(float time)
        {
            LeanTween.value(waveProgressBar.gameObject, f => waveProgressBar.value = f, 1.0f, 0.0f, time);
            if (currentState != ProgressBarState.Wave)
            {
                cooldownProgressBar.gameObject.SetActive(false);
                waveProgressBar.gameObject.SetActive(true);
                currentState = ProgressBarState.Wave;
            }
        }

        private void StartCooldown(float time)
        {
            LeanTween.value(cooldownProgressBar.gameObject, f => cooldownProgressBar.value = f, 1.0f, 0.0f, time);
            if (currentState != ProgressBarState.Cooldown)
            {
                waveProgressBar.gameObject.SetActive(false);
                cooldownProgressBar.gameObject.SetActive(true);
                currentState = ProgressBarState.Cooldown;
            }
        }

        private void HideBars()
        {
            waveProgressBar.gameObject.SetActive(false);
            waveProgressBar.value = 0;

            cooldownProgressBar.gameObject.SetActive(false);
            cooldownProgressBar.value = 0;

            currentState = ProgressBarState.None;
        }

        private bool IsCooldownRunning()
        {
            return currentState == ProgressBarState.Cooldown;
        }

        private bool IsWaveRunning()
        {
            return currentState == ProgressBarState.Wave;
        }
    }
}