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

                // Check if entity health is below 0 and it is alive
                if(healthComponent.Current <= 0 && healthComponent.IsAlive)
                {
                    // Entity is dead
                    healthComponent.IsAlive = false;

                    // See if it is a player..
                    // Give experience penalty
                    // Reset health
                    // Move to graveyard location?!
                    // show some information to the player!?
                    if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                    {
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
                        
                    }
                    
                    // Else if it is an AI entity.
                    // Will the attacker get experience?
                    else if(cm.HasEntityComponent<AIComponent>(entity.Key))
                    {
                        // Enemy dead
                        //cm.GetComponentForEntity<LevelComponent>(cm.GetComponentForEntity<DamageComponent>(entity.Key).LastAttacker).ExperienceGains.Add(entity.Key);
                        //cm.RemoveComponentFromEntity<MoveComponent>(entity.Key);
                        //cm.RemoveComponentFromEntity<AIComponent>(entity.Key);
                        //cm.RemoveComponentFromEntity<SoundComponent>(entity.Key);
                        //cm.RemoveComponentFromEntity<AttackComponent>(entity.Key);
                        //cm.AddComponentsToEntity(entity.Key,
                        //    new IComponent[] {
                        //    new InteractComponent(InteractType.Trap),
                        //    new AttackComponent(10, 0.0f, 0.0f, WeaponType.Sword) }
                        //    );
                        cm.RemoveEntity(entity.Key);
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

                // Health is not below 0 and the entity is alive.
                // will it regenerate health?
                // if it is a player?
                // if it is an AI ?
                else
                {
                    // TODO
                }
            }
        }
    }
}
