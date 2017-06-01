using Game.Components;
using Game.Managers;
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
                {
                    if (cm.HasEntityComponent<ItemComponent>(entity.Key))
                        cm.RemoveComponentFromEntity<HealthComponent>(entity.Key);
                    else
                        cm.RemoveEntity(entity.Key);
                }

                if (healthComponent.IncomingDamage.Count > 0)
                {
                    foreach (int damage in healthComponent.IncomingDamage)
                    {
                        int reduction = (int)(1 + damage / ((healthComponent.DamageReduction[0] + healthComponent.DamageReduction[1]) * 0.03f + 1));
                        ApplyDamageToEntity(entity.Key, reduction);
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Hurt"].Action = SoundAction.Play;
                        if (cm.HasEntityComponent<KnockbackComponent>(entity.Key) && cm.HasEntityComponent<MoveComponent>(entity.Key))
                        {
                            ApplyKnockbackToEntity(entity.Key, healthComponent.LastAttacker, damage, gameTime);
                        }
                    }
                    healthComponent.IncomingDamage.Clear();
                }
                // Check if the entity health is below 0 and it is alive
                if (healthComponent.Current <= 0 && healthComponent.IsAlive)
                {
                    healthComponent.IsAlive = false;

                    // If the entity is a player
                    if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                    {
                        // Give experience penalty
                        LevelComponent levelComponent = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        levelComponent.LevelLoss = true;

                        if (levelComponent.CurrentLevel - 1 > 0)
                        {
                            healthComponent.IsAlive = true;
                            healthComponent.Current = healthComponent.Max;
                        }
                        else
                        {
                            if (cm.HasEntityComponent<InventoryComponent>(entity.Key))
                            {
                                InventoryComponent invencomp = cm.GetComponentForEntity<InventoryComponent>(entity.Key);
                                foreach(int i in invencomp.Items)
                                    cm.RemoveEntity(i);
                                foreach(int i in invencomp.WeaponBodyHead)
                                    cm.RemoveEntity(i);
                            }
                            cm.RemoveEntity(entity.Key);
                        }

                        // TODO
                        // Move player to graveyard location
                        //cm.GetComponentForEntity<PositionComponent>(entity.Key).position = GraveYardPos;

                        //TODO
                        // show some information to the player
                        // write something in a dialogbox that he died etc..
                        bool gameOver = false;
                        foreach (var player in cm.GetComponentsOfType<PlayerControlComponent>())
                        {
                            HealthComponent playerHealthComp = cm.GetComponentForEntity<HealthComponent>(player.Key);
                            if (playerHealthComp.IsAlive)
                            {
                                gameOver = false;
                                break;
                            }
                            gameOver = true;   
                        }
                        if (gameOver)
                        {
                            GameStateManager.GetInstance().State = GameState.GameOver;
                        }
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
                        //cm.RemoveComponentFromEntity<HealthComponent>(entity.Key);
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
            knockbackComponent.KnockbackDir = Vector2.Normalize(newDir * attackDmg +  new Vector2(5, 5));
            knockbackComponent.Cooldown = attackDmg / 40.0f;
            knockbackComponent.KnockbackActive = true;
        }
    }
}
