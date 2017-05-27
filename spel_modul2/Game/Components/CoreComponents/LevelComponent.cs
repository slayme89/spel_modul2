using GameEngine.Components;
using System;
using System.Collections.Generic;

namespace Game.Components
{
    public class LevelComponent : IComponent
    {
        public int StartLevel { get; set; } = 1;
        public int CurrentLevel { get; set; }
        public int TotalExperience { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public bool LevelLoss { get; set; } = false;
        public List<int> ExperienceGains { get; set; }

        public LevelComponent(int startLevel)
        {
            StartLevel = startLevel;
            CurrentLevel = startLevel;
            ExperienceGains = new List<int>();
        }

        public LevelComponent(int startLevel, int startExperience)
        {
            StartLevel = startLevel;
            CurrentLevel = startLevel;
            TotalExperience = startExperience;
            ExperienceGains = new List<int>();
        }

        public object Clone()
        {
            LevelComponent o = (LevelComponent)MemberwiseClone();
            o.ExperienceGains = new List<int>(ExperienceGains);
            return o;
        }
    }
}
