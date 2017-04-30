using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEngine
{
    class AttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var Entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(Entity.Key);
                if (attackComponent.IsAttacking)
                {
                    attackComponent.IsAttacking = false;
                    EntityAttack(Entity.Key);
                }
            }
        }

        public void EntityAttack(int entity)
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
                        
                        //Debug.WriteLine(entityHit);
                        var entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
                        if (entityHitHealth != null && entityHitHealth.Current > 0)
                        {
                            //apply damage to entitieshit
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
