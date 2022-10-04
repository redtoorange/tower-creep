using System;

namespace TowerCreep.Player.TowerCollection
{
    public class TowerProgressionData
    {
        public Action OnDataChange;
        
        public int CurrentLevel = 1;
        public int CurrentExperience = 0;
        public int RequiredExperience = 100;

        public float GetExperiencePercent()
        {
            return CurrentExperience / (float)RequiredExperience;
        }

        public void GiveExperience(int amount)
        {
            CurrentExperience += amount;
            if (CurrentExperience >= RequiredExperience)
            {
                CurrentLevel++;
                CurrentExperience -= RequiredExperience;
                RequiredExperience *= 2;
            }
            
            OnDataChange?.Invoke();
        }
    }
}