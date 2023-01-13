using System;

namespace TowerCreep.TowerCreep2D.Scripts.Player
{
    [Serializable]
    public class PlayerResource
    {
        public int currentValue;

        public PlayerResource(int currentValue)
        {
            this.currentValue = currentValue;
        }
    }
}