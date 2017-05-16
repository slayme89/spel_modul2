using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class LevelSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<LevelComponent>())
            {
                LevelComponent levelComponent = (LevelComponent)entity.Value;
                if (cm.HasEntityComponent<StatsComponent>(entity.Key))
                {
                    StatsComponent statComponent = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                    //See if there is any experience to gain
                    if (levelComponent.ExperienceLoss.Count > 0)
                    {
                        foreach (int xpLoss in levelComponent.ExperienceLoss)
                        {
                            levelComponent.Experience += xpLoss;
                            int oldLevel = levelComponent.CurrentLevel;
                            int newLevel = LevelCalculator(levelComponent.Experience);
                            levelComponent.CurrentLevel = newLevel;

                            // See if entity lost level
                            if (oldLevel > newLevel)
                            {
                                int num = oldLevel - newLevel;
                                statComponent.RemoveStats += 3 * num;
                            }
                            // Permadeath, Remove entity and everything assosiated with that entity
                            if (levelComponent.CurrentLevel == 0)
                            {
                                cm.RemoveEntity(entity.Key);
                            }
                        }
                        levelComponent.ExperienceLoss = new List<int>();
                    }

                    
                    if (levelComponent.ExperienceGains.Count > 0)
                    {
                        foreach (int entityKilled in levelComponent.ExperienceGains)
                        {
                            int xpGained = CalculateKillExperience(entity.Key, entityKilled);
                            levelComponent.Experience += xpGained;
                            int oldLevel = levelComponent.CurrentLevel;
                            int newLevel = LevelCalculator(levelComponent.Experience);
                            levelComponent.CurrentLevel = newLevel;

                            // See if entity leveled up
                            if (oldLevel < newLevel)
                            {
                                int num = newLevel - oldLevel;
                                statComponent.SpendableStats += 3 * num;
                            }
                        }
                        levelComponent.ExperienceGains = new List<int>();
                    }
                }
            }
        }

        private int CalculateKillExperience(int entity, int entityKilled)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            LevelComponent entityToGainExpComponent = cm.GetComponentForEntity<LevelComponent>(entity);
            int entityKilledLvl = cm.GetComponentForEntity<LevelComponent>(entityKilled).CurrentLevel;
            int entitylvl = entityToGainExpComponent.CurrentLevel;
            int xpGained = 22 * (entityKilledLvl - entitylvl + 1);
            if (xpGained <= 0)
                xpGained = 2;

            return xpGained;
        }

        private int LevelCalculator(int experience)
        {
            if (experience <= 0) return 0;
            else if (experience <= 83) return 1;
            else if (experience <= 174) return 2;
            else if (experience <= 266) return 3;
            else if (experience <= 389) return 4;
            else if (experience <= 572) return 5;
            else if (experience <= 939) return 6;
            else if (experience <= 1306) return 7;
            else if (experience <= 1673) return 8;
            else if (experience <= 2407) return 9;
            else return 10;
        }
    }
}
