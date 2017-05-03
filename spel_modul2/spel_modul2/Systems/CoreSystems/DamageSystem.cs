using Microsoft.Xna.Framework;

namespace GameEngine
{
    class DamageSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
        }

        public void Update(int entityHit, int attackingEntity)
        {
            ApplyDamageToEntity(entityHit, attackingEntity);
        }

        private void ApplyDamageToEntity(int entityHit, int attackingEntity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
            AttackComponent attackingEntityDamage = cm.GetComponentForEntity<AttackComponent>(attackingEntity);

            entityHitHealth.Current -= attackingEntityDamage.Damage;
            if(entityHitHealth.Current <= 0)
            {
                entityHitHealth.IsAlive = false;
            } 
        }
    }
}
