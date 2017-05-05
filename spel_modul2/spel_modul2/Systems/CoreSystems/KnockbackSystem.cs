using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class KnockbackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<DamageComponent>())
            {
                DamageComponent damageComponent = (DamageComponent)entity.Value;
                foreach (int attackingEntity in damageComponent.IncomingDamageEntityID)
                {
                    ApplyKnockbackToEntity(entity.Key, attackingEntity);
                    damageComponent.LastAttacker = attackingEntity;
                }
                if (damageComponent.IncomingDamageEntityID.Count > 0)
                    damageComponent.IncomingDamageEntityID = new List<int>();
            }
        }

        private void ApplyKnockbackToEntity(int entityHit, int attackingEntity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            // Påverkas av stats??? JAA!
            entityHitHealth.Current -= attackingEntityDamage.Damage;

        }
    }
}
