﻿using System;

namespace TowerCreep.Player.TowerCollection
{
    public class TowerInstanceProgressionData
    {
        public Action OnTowerInstanceProgressionChange;
        public Action OnTowerInstanceLevelChange;

        public int CurrentLevel
        {
            get => currentLevel;
            private set => currentLevel = value;
        }
        private int currentLevel = 1;


        public int CurrentExperience
        {
            get => currentExperience;
            private set => currentExperience = value;
        }
        public int currentExperience = 0;

        public int RequiredExperience
        {
            get => requiredExperience;
            private set => requiredExperience = value;
        }
        public int requiredExperience = 100;

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
                OnTowerInstanceLevelChange?.Invoke();
            }

            OnTowerInstanceProgressionChange?.Invoke();
        }
    }
}