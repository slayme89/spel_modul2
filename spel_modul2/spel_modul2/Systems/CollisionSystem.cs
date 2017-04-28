using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class CollisionSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
          
        }

        void DetectCollisions()
        {
            var cm = ComponentManager.GetInstance();

            foreach (KeyValuePair<int, IComponent> entity1 in cm.GetComponentsOfType<CollisionComponent>())
            {
                CollisionComponent collisionBox1 = ComponentManager.GetInstance().GetComponentForEntity<CollisionComponent>(entity1.Key);

                foreach (KeyValuePair<int, IComponent> entity2 in cm.GetComponentsOfType<CollisionComponent>())
                {
                    CollisionComponent collisionBox2 = ComponentManager.GetInstance().GetComponentForEntity<CollisionComponent>(entity2.Key);

                    if(collisionBox1 != collisionBox2 && collisionBox1.collisionBox.Intersects(collisionBox2.collisionBox))
                    {
                        //krock har skett, kolla vidare om de är en spelare eller ngt annat.
                        
                    }
                }
            }
        }
    }
}
