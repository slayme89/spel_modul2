using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if(healthComponent.Current <= 0 && healthComponent.IsAlive)
                {
                    // Dead
                    healthComponent.IsAlive = false;
                    if(cm.GetComponentForEntity<PlayerComponent>(entity.Key) == null)
                    {
                        // Enemy dead
                        cm.GetComponentForEntity<LevelComponent>(cm.GetComponentForEntity<DamageComponent>(entity.Key).LastAttacker).ExperienceGains.Add(entity.Key);
                    }
                    else
                    {
                        // Player dead
                        LevelComponent levelComponent = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        switch (levelComponent.CurrentLevel)
                        {
                            case 1: levelComponent.ExperienceLoss.Add(-45);  break;
                            case 2: levelComponent.ExperienceLoss.Add(-46);  break;
                            case 3: levelComponent.ExperienceLoss.Add(-61);  break;
                            case 4: levelComponent.ExperienceLoss.Add(-91);  break;
                            case 5: levelComponent.ExperienceLoss.Add(-183); break;
                            case 6: levelComponent.ExperienceLoss.Add(-183); break;
                            case 7: levelComponent.ExperienceLoss.Add(-185); break;
                            case 8: levelComponent.ExperienceLoss.Add(-367); break;
                            case 9: levelComponent.ExperienceLoss.Add(-400); break;
                            case 10:levelComponent.ExperienceLoss.Add(-400); break;
                        }
                    }



                    // Kill the entity
                    //AnimationComponent animationComponent = cm.GetComponentForEntity<AnimationComponent>(entity.Key);
                    //if (animationComponent != null)
                    //    animationComponent.isPaused = !animationComponent.isPaused;
                    //cm.RemoveComponentFromEntity<MoveComponent>(entity.Key);
                    //cm.RemoveComponentFromEntity<AIComponent>(entity.Key);
                    //cm.RemoveComponentFromEntity<CollisionComponent>(entity.Key);
                    //cm.RemoveComponentFromEntity<SoundComponent>(entity.Key);
                    //cm.RemoveComponentFromEntity<AttackComponent>(entity.Key);
                }
                else
                {
                    // Regenerate health?
                }
            }
        }
    }
}
