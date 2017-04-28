using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class CollisionSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
          
        }

        public bool DetectMovementCollision(int entity, Point position)
        {
            var cm = ComponentManager.GetInstance();
            CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity);
            Rectangle rectToCheck = new Rectangle(position, collisionComponent.collisionBox.Size);

            foreach (KeyValuePair<int, IComponent> entity2 in cm.GetComponentsOfType<CollisionComponent>())
            {
                if(entity2.Key != entity)
                {
                    CollisionComponent collisionComponent2 = cm.GetComponentForEntity<CollisionComponent>(entity2.Key);
                    collisionComponent2.collisionBox.Location = cm.GetComponentForEntity<PositionComponent>(entity2.Key).position;
                    if (rectToCheck.Intersects(collisionComponent2.collisionBox))
                    {
                        //Collision detected
                        return true;
                    }
                }  
            }
            return false;
        }
    }
}
