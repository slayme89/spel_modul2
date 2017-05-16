using Microsoft.Xna.Framework;

namespace GameEngine
{
    class HealthSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<HealthComponent>())
            {
                HealthComponent healthComponent = (HealthComponent)entity.Value;

                // Check if the entity health is below 0 and it is alive
                if(healthComponent.Current <= 0 && healthComponent.IsAlive)
                {
                    // Entity died
                    healthComponent.IsAlive = false;

                    // If the entity is a player
                    if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                    {
                        // Give experience penalty
                        LevelComponent levelComponent = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        switch (levelComponent.CurrentLevel)
                        {
                            case 1: levelComponent.ExperienceLoss.Add(-45); break;
                            case 2: levelComponent.ExperienceLoss.Add(-46); break;
                            case 3: levelComponent.ExperienceLoss.Add(-61); break;
                            case 4: levelComponent.ExperienceLoss.Add(-91); break;
                            case 5: levelComponent.ExperienceLoss.Add(-183); break;
                            case 6: levelComponent.ExperienceLoss.Add(-183); break;
                            case 7: levelComponent.ExperienceLoss.Add(-185); break;
                            case 8: levelComponent.ExperienceLoss.Add(-367); break;
                            case 9: levelComponent.ExperienceLoss.Add(-400); break;
                            case 10: levelComponent.ExperienceLoss.Add(-400); break;
                        }
                        // TODO
                        // Move player to graveyard location
                        //cm.GetComponentForEntity<PositionComponent>(entity.Key).position = GraveYardPos;

                        //TODO
                        // show some information to the player
                        // write something in a dialogbox that he died etc...

                        // Reset health and make the player alive again
                        healthComponent.IsAlive = true;
                        healthComponent.Current = healthComponent.Max;

                    }

                    //If the entity is a NPC
                    else if (cm.HasEntityComponent<AIComponent>(entity.Key))
                    {
                        // Give the last attacker experience points
                        cm.GetComponentForEntity<LevelComponent>(cm.GetComponentForEntity<DamageComponent>(entity.Key).LastAttacker).ExperienceGains.Add(entity.Key);
                        // Remove the entity
                        cm.RemoveEntity(entity.Key);
                    }
                }
            }
        }
    }
}
