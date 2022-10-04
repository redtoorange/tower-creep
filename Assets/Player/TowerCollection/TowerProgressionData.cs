namespace TowerCreep.Player.TowerCollection
{
    public class TowerProgressionData
    {
        public int CurrentLevel = 1;
        public int CurrentExperience = 0;
        public int RequiredExperience = 100;

        public float GetExperiencePercent()
        {
            return CurrentExperience / (float)RequiredExperience;
        }
    }
}