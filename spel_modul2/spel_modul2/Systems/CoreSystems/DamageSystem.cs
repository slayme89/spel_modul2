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
                    ApplyDamageToEntity(entity.Key, attackingEntity);
                    damageComponent.LastAttacker = attackingEntity;
                    if (cm.HasEntityComponent<KnockbackComponent>(entity.Key) && cm.HasEntityComponent<MoveComponent>(entity.Key))
                    {
                        ApplyKnockbackToEntity(entity.Key, attackingEntity);
                }
                }
                if(damageComponent.IncomingDamageEntityID.Count > 0)
                    damageComponent.IncomingDamageEntityID = new List<int>();
            }
        }

        private void ApplyDamageToEntity(int entityHit, int attackingEntity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            // Påverkas av stats??? JAA!
            if(entityHit != attackingEntity)
            entityHitHealth.Current -= attackingEntityDamage.Damage;
        }
            
        private void ApplyKnockbackToEntity(int entityHit, int attackingEntity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entityHit);
            Vector2 posCompAttacker = cm.GetComponentForEntity<PositionComponent>(attackingEntity).position;
            int attackDmg = cm.GetComponentForEntity<AttackComponent>(attackingEntity).Damage;
            int knockbackWeight = cm.GetComponentForEntity<KnockbackComponent>(entityHit).Knockback;

            Vector2 newDir = new Vector2(posComp.position.X - posCompAttacker.X, posComp.position.Y - posCompAttacker.Y);
            int length = (int)Math.Sqrt(Math.Pow(newDir.X, 2.0) + Math.Pow(newDir.Y, 2.0));
            newDir = newDir / length;
            posComp.position += (newDir * knockbackWeight * attackDmg);
        }
    }
}
