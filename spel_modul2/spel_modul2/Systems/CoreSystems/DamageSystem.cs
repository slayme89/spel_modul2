using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    class DamageSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<DamageComponent>())
            {
                DamageComponent damageComponent = (DamageComponent)entity.Value;
                foreach (int attackingEntity in damageComponent.IncomingDamageEntityID)
                {
                    if (entity.Key != attackingEntity)
                    {
                        ApplyDamageToEntity(entity.Key, attackingEntity);
                        damageComponent.LastAttacker = attackingEntity;
                        if (cm.HasEntityComponent<KnockbackComponent>(entity.Key) && cm.HasEntityComponent<MoveComponent>(entity.Key))
                        {
                            ApplyKnockbackToEntity(entity.Key, attackingEntity, gameTime);
                        }
                    }
                }
                if (damageComponent.IncomingDamageEntityID.Count > 0)
                    damageComponent.IncomingDamageEntityID = new List<int>();
            }
        }

        private void ApplyDamageToEntity(int entityHit, int attackingEntity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            entityHitHealth.Current -= attackingEntityDamage.Damage;
        }

        // Fix the "teleport-effect", make it smooth!
        private void ApplyKnockbackToEntity(int entityHit, int attackingEntity, GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            KnockbackComponent knockbackComponent = cm.GetComponentForEntity<KnockbackComponent>(entityHit);
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entityHit);
            Vector2 posCompAttacker = cm.GetComponentForEntity<PositionComponent>(attackingEntity).position;
            int attackDmg = cm.GetComponentForEntity<AttackComponent>(attackingEntity).Damage;

            Vector2 newDir = new Vector2(posComp.position.X - posCompAttacker.X, posComp.position.Y - posCompAttacker.Y);

            //posComp.position += (newDir * knockbackWeight * attackDmg);
            knockbackComponent.KnockbackDir = Vector2.Normalize(newDir * attackDmg);
            knockbackComponent.Cooldown = attackDmg / 40.0f;
            knockbackComponent.KnockbackActive = true;
        }
    }
}
