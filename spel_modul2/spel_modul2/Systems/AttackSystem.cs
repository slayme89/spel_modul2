using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class AttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            
        }

        public void entityAttack(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity);
            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity);
            PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity);
            if(attackComponent.Type == WeaponType.Sword)
            {
                CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity);
                int range = collisionComponent.collisionBox.Size.X / 2;
                Rectangle hitArea = new Rectangle(positionComponent.position + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size);

                List<int> entitiesHit = CollisionSystem.DetectAreaCollision(hitArea);

                foreach(int entityHit in entitiesHit)
                {
                    
                }
                //apply damage to entitieshit
            }else if (attackComponent.Type == WeaponType.Bow)
            {

            }else if (attackComponent.Type == WeaponType.Magic)
            {

            }
        }
    }
}
