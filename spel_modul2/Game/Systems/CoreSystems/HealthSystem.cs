using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;

namespace Game.Systems
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
                if (healthComponent.DeathTimer > 0 && !healthComponent.IsAlive && cm.HasEntityComponent<AIComponent>(entity.Key))
                    healthComponent.DeathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Remove the entity when the deathtimer expires
                if (healthComponent.DeathTimer <= 0.0f && !healthComponent.IsAlive && cm.HasEntityComponent<AIComponent>(entity.Key))
                    cm.RemoveEntity(entity.Key);

                foreach (int damage in healthComponent.IncomingDamage)
                {
                    if (entity.Key != damage)
                    {
                        int reduction = (int)(1 + damage / ((healthComponent.DamageReduction[0] + healthComponent.DamageReduction[1]) * 0.03f + 1));
                        ApplyDamageToEntity(entity.Key, reduction);
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayDamageSound = true;
                        if (cm.HasEntityComponent<KnockbackComponent>(entity.Key) && cm.HasEntityComponent<MoveComponent>(entity.Key))
                        {
                            ApplyKnockbackToEntity(entity.Key, healthComponent.LastAttacker, damage, gameTime);
                        }
                    }
                }
                if (healthComponent.IncomingDamage.Count > 0)
                    healthComponent.IncomingDamage.Clear();


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
                        else 
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
                        cm.GetComponentForEntity<LevelComponent>(healthComponent.LastAttacker).ExperienceGains.Add(entity.Key);
                        
                        //Set animation to deathAnimation
                        cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key).ActiveAnimation = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key).Animations.Length - 1;

                        // Cant attack
                        cm.RemoveComponentFromEntity<AttackComponent>(entity.Key);
                        // cant move
                        cm.RemoveComponentFromEntity<MoveComponent>(entity.Key);
                        cm.RemoveComponentFromEntity<KnockbackComponent>(entity.Key);
                        cm.RemoveComponentFromEntity<CollisionComponent>(entity.Key);
                        cm.RemoveComponentFromEntity<HealthComponent>(entity.Key);
                        //cm.RemoveComponentFromEntity<AIComponent>(entity.Key);
                    }
                }
            }
        }

        // Damage
        private void ApplyDamageToEntity(int entityHit, int damage)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            //AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            entityHitHealth.Current -= damage;
        }

        // Knockback
        private void ApplyKnockbackToEntity(int entityHit, int attacker, int damage, GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            KnockbackComponent knockbackComponent = cm.GetComponentForEntity<KnockbackComponent>(entityHit);
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entityHit);
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entityHit);
            Vector2 posCompAttacker = cm.GetComponentForEntity<PositionComponent>(attacker).Position;
            int attackDmg = damage;

            Vector2 newDir = new Vector2(posComp.Position.X - posCompAttacker.X, posComp.Position.Y - posCompAttacker.Y);

            knockbackComponent.prevDir = moveComp.Direction.ToVector2();
            knockbackComponent.KnockbackDir = Vector2.Normalize(newDir * attackDmg);
            knockbackComponent.Cooldown = attackDmg / 40.0f;
            knockbackComponent.KnockbackActive = true;
        }
    }
}
