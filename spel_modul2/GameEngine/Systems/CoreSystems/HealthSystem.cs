using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public class HealthSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<HealthComponent>())
            {
                HealthComponent healthComponent = (HealthComponent)entity.Value;

                // Dec deathtimer
                if (healthComponent.DeathTimer > 0 && healthComponent.IsAlive == false && cm.HasEntityComponent<AIComponent>(entity.Key))
                    healthComponent.DeathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Remove the entity when the deathtimer expires
                if (healthComponent.DeathTimer <= 0.0f)
                    cm.RemoveEntity(entity.Key);

                // Check if the entity health is below 0 and it is alive
                if (healthComponent.Current <= 0 && healthComponent.IsAlive)
                {
                    healthComponent.IsAlive = false;

                    // If the entity is a player
                    if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                    {
                        // Give experience penalty
                        LevelComponent levelComponent = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        switch (levelComponent.CurrentLevel)
                        {
                            // if player is level 0, he dies
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

                        if (levelComponent.CurrentLevel > 0)
                        {
                            healthComponent.IsAlive = true;
                            healthComponent.Current = healthComponent.Max;
                        }
                        else if(levelComponent.CurrentLevel <= 0)
                            cm.RemoveEntity(entity.Key);

                        // TODO
                        // Move player to graveyard location
                        //cm.GetComponentForEntity<PositionComponent>(entity.Key).position = GraveYardPos;

                        //TODO
                        // show some information to the player
                        // write something in a dialogbox that he died etc..
                    }

                    //If the entity is a NPC
                    else if (cm.HasEntityComponent<AIComponent>(entity.Key))
                    {
                        // Give the last attacker experience points
                        cm.GetComponentForEntity<LevelComponent>(cm.GetComponentForEntity<DamageComponent>(entity.Key).LastAttacker).ExperienceGains.Add(entity.Key);
                        //Set animation to deathAnimation
                        cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key).ActiveAnimation = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key).Animations.Length - 1;
                        
                        // Cant attack
                        if (cm.HasEntityComponent<AttackComponent>(entity.Key))
                            cm.GetComponentForEntity<AttackComponent>(entity.Key).CanAttack = false;
                        // cant move
                        if (cm.HasEntityComponent<MoveComponent>(entity.Key))
                            cm.GetComponentForEntity<MoveComponent>(entity.Key).CanMove = false;
                    }
                }
            }
        }
    }
}
