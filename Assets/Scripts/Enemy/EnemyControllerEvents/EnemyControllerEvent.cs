namespace TowerCreep.Enemy.EnemyControllerEvents
{
    public enum EnemyControllerEventType
    {
        WaveStart,
        WaveEnded,
        CooldownStarted,
        CooldownEnded,
        AllWavesComplete
    }

    public class EnemyControllerEvent
    {
        public EnemyController controller;
        public EnemyControllerEventType type;

        public EnemyControllerEvent(EnemyController controller, EnemyControllerEventType type)
        {
            this.controller = controller;
            this.type = type;
        }
    }

    public class EnemyControlledTimedEvent : EnemyControllerEvent
    {
        public float length;

        public EnemyControlledTimedEvent(EnemyController controller, EnemyControllerEventType type, float length) :
            base(controller, type)
        {
            this.length = length;
        }
    }
}