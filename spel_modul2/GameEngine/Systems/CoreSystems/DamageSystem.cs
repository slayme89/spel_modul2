using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine.Systems
{
    public class DamageSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<DamageComponent>())
            {
                DamageComponent damageComponent = (DamageComponent)entity.Value;
                foreach (int damage in damageComponent.IncomingDamage)
                {
                    if (entity.Key != damage)
                    {
                        ApplyDamageToEntity(entity.Key, damage);
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayDamageSound = true;
                        if (cm.HasEntityComponent<KnockbackComponent>(entity.Key) && cm.HasEntityComponent<MoveComponent>(entity.Key))
                        {
                            ApplyKnockbackToEntity(entity.Key, damageComponent.LastAttacker, damage, gameTime);
                        }
                    }
                }
                if (damageComponent.IncomingDamage.Count > 0)
                    damageComponent.IncomingDamage = new List<int>();
            }
        }

        private void ApplyDamageToEntity(int entityHit, int damage)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            //AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            entityHitHealth.Current -= damage;
        }
        
        private void ApplyKnockbackToEntity(int entityHit, int attacker, int damage, GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            KnockbackComponent knockbackComponent = cm.GetComponentForEntity<KnockbackComponent>(entityHit);
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entityHit);
            Vector2 posCompAttacker = cm.GetComponentForEntity<PositionComponent>(attacker).Position;
            int attackDmg = damage;

            Vector2 newDir = new Vector2(posComp.Position.X - posCompAttacker.X, posComp.Position.Y - posCompAttacker.Y);
            
            knockbackComponent.KnockbackDir = Vector2.Normalize(newDir * attackDmg);
            knockbackComponent.Cooldown = attackDmg / 40.0f;
            knockbackComponent.KnockbackActive = true;
        }
    }
}
