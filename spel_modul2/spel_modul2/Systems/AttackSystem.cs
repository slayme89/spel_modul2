using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class AttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                if (attackComponent.IsAttacking)
                {
                    attackComponent.IsAttacking = false;
                    EntityAttack(gameTime, entity.Key);
                }
            }
        }

        public void EntityAttack(GameTime gameTime, int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity);
            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity);
            PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity);
            if(attackComponent.Type == WeaponType.Sword)
            {
                CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity);
                int range = collisionComponent.collisionBox.Size.X;
                Rectangle hitArea = new Rectangle(positionComponent.position + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size);

                List<int> entitiesHit = CollisionSystem.DetectAreaCollision(hitArea);
                foreach (int entityHit in entitiesHit)
                {
                    if(entityHit != entity)
                    {
                        var entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
                        if (entityHitHealth.IsAlive == true && entityHitHealth != null)
                        {
                            //apply damage to the entity that was hit via the DamageSystem
                            DamageSystem dmgSys = new DamageSystem();
                            dmgSys.Update(gameTime, entityHit, entity);
                        }
                    }
                }
            }else if (attackComponent.Type == WeaponType.Bow)
            {

            }else if (attackComponent.Type == WeaponType.Magic)
            {

            }
        }
    }
}
