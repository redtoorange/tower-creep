using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerCreep.Interface.LevelProgress
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

        [SerializeField] private ProgressBar waveProgressBar;
        [SerializeField] private ProgressBar cooldownProgressBar;
        // private Tween.TransitionType progressBarTransition = Tween.TransitionType.Linear;
        // private Tween tweener;

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
            // if (tweener.IsActive())
            // {
            //     tweener.StopAll();
            // }
            //
            // tweener.InterpolateProperty(waveProgressBar, "value", 0, 100, time, progressBarTransition);
            // tweener.Start();

            if (currentState != ProgressBarState.Wave)
            {
                cooldownProgressBar.SetEnabled(false);
                waveProgressBar.SetEnabled(true);
                currentState = ProgressBarState.Wave;
            }
        }

        private void StartCooldown(float time)
        {
            // if (tweener.IsActive())
            // {
            //     tweener.StopAll();
            // }
            //
            // tweener.InterpolateProperty(cooldownProgressBar, "value", 0, 100, time, progressBarTransition);
            // tweener.Start();

            if (currentState != ProgressBarState.Cooldown)
            {
                waveProgressBar.SetEnabled(false);
                cooldownProgressBar.SetEnabled(true);
                currentState = ProgressBarState.Cooldown;
            }
        }

        private void HideBars()
        {
            waveProgressBar.SetEnabled(false);
            waveProgressBar.value = 0;

            cooldownProgressBar.SetEnabled(false);
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