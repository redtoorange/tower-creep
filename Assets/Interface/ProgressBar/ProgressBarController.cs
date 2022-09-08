using Godot;
using TowerCreep.Enemy;
using TowerCreep.Enemy.EnemyControllerEvents;

namespace TowerCreep.Interface.LevelProgress
{
    public class ProgressBarController : Node
    {
        private enum ProgressBarState
        {
            None,
            Wave,
            Cooldown
        }

        private ProgressBarState currentState = ProgressBarState.None;

        private TextureProgress waveProgressBar;
        private TextureProgress cooldownProgressBar;
        private Tween.TransitionType progressBarTransition = Tween.TransitionType.Linear;
        private Tween tweener;

        public override void _Ready()
        {
            waveProgressBar = GetNode<TextureProgress>("WaveProgress");
            cooldownProgressBar = GetNode<TextureProgress>("CooldownProgress");
            tweener = GetNode<Tween>("Tween");

            HideBars();

            EnemyController.OnEnemyControllerEvent += HandleEnemyControllerEvent;
        }

        public override void _ExitTree()
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
            else if(ece.type == EnemyControllerEventType.AllWavesComplete)
            {
                HideBars();
            }
        }

        private void StartWave(float time)
        {
            if (tweener.IsActive())
            {
                tweener.StopAll();
            }

            tweener.InterpolateProperty(waveProgressBar, "value", 0, 100, time, progressBarTransition);
            tweener.Start();

            if (currentState != ProgressBarState.Wave)
            {
                cooldownProgressBar.Visible = false;
                waveProgressBar.Visible = true;
                currentState = ProgressBarState.Wave;
            }
        }

        private void StartCooldown(float time)
        {
            if (tweener.IsActive())
            {
                tweener.StopAll();
            }

            tweener.InterpolateProperty(cooldownProgressBar, "value", 0, 100, time, progressBarTransition);
            tweener.Start();

            if (currentState != ProgressBarState.Cooldown)
            {
                waveProgressBar.Visible = false;
                cooldownProgressBar.Visible = true;
                currentState = ProgressBarState.Cooldown;
            }
        }

        private void HideBars()
        {
            waveProgressBar.Visible = false;
            waveProgressBar.Value = 0;

            cooldownProgressBar.Visible = false;
            cooldownProgressBar.Value = 0;

            currentState = ProgressBarState.None;
        }

        private bool IsCooldownRunning()
        {
            return currentState == ProgressBarState.Cooldown && tweener.IsActive();
        }

        private bool IsWaveRunning()
        {
            return currentState == ProgressBarState.Wave && tweener.IsActive();
        }
    }
}