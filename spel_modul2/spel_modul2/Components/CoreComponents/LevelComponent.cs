using System.Collections.Generic;

namespace GameEngine
{
    class LevelComponent : IComponent
    {
        public int StartLevel { get; set; } = 1;
        public int CurrentLevel { get; set; }
        public int Experience { get; set; } = 0;
        public List<int> ExperienceGains { get; set; }
        public List<int> ExperienceLoss { get; set; }

        public LevelComponent(int startLevel)
        {
            StartLevel = startLevel;
            CurrentLevel = startLevel;
            ExperienceGains = new List<int>();
            ExperienceLoss = new List<int>();
        }

        public LevelComponent(int startLevel, int startExperience)
        {
            StartLevel = startLevel;
            CurrentLevel = startLevel;
            Experience = startExperience;
            ExperienceGains = new List<int>();
            ExperienceLoss = new List<int>();
        }
    }
}
