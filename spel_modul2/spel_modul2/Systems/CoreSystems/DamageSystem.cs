using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

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
                }
                if(damageComponent.IncomingDamageEntityID.Count > 0)
                    damageComponent.IncomingDamageEntityID = new List<int>();
            }
        }

        private void ApplyDamageToEntity(int entityHit, int attackingEntity)
        {
            Debug.WriteLine("entityHit " + entityHit + " attackingEntity " + attackingEntity);
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            // Påverkas av stats???
            entityHitHealth.Current -= attackingEntityDamage.Damage;
            
        }
    }
}
