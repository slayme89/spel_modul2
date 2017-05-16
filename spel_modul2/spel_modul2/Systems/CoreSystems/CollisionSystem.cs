using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEngine
{
    class CollisionSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            // Detect hit collision
            foreach (var entity in cm.GetComponentsOfType<CollisionComponent>())
            {
                CollisionComponent collisionComponent = (CollisionComponent)entity.Value;
                Rectangle rect = collisionComponent.attackCollisionBox;
                PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                if (collisionComponent.checkAttackColision)
                {
                    foreach (int entityID in DetectAreaCollision(rect))
                    {
                        DamageComponent damageComponent = cm.GetComponentForEntity<DamageComponent>(entityID);
                        if (damageComponent != null)
                        {
                            damageComponent.IncomingDamageEntityID.Add(entity.Key);
                        }
                    }
                }
            }
        }

        public static List<int> DetectAreaCollision(Rectangle area)
        {
            var cm = ComponentManager.GetInstance();
            List<int> foundEntities = new List<int>();
            foreach (var entity in cm.GetComponentsOfType<CollisionComponent>())
            {
                CollisionComponent collisionComponent = (CollisionComponent)entity.Value;

                if (area.Intersects(collisionComponent.collisionBox))
                {
                    //Collision detected, add them to the list
                    foundEntities.Add(entity.Key);
                }
            }
            return foundEntities;
        }

        //Detect if characters collide
        public bool DetectMovementCollision(int entity, Vector2 position)
        {
            var cm = ComponentManager.GetInstance();
            CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity);
            Rectangle rectToCheck = new Rectangle(new Vector2(position.X - (collisionComponent.collisionBox.Width / 2), position.Y - (collisionComponent.collisionBox.Height / 2)).ToPoint(), collisionComponent.collisionBox.Size);

            foreach (var entity2 in cm.GetComponentsOfType<CollisionComponent>())
            {
                if (entity2.Key != entity)
                {
                    CollisionComponent collisionComponent2 = (CollisionComponent)entity2.Value;
                    if (cm.HasEntityComponent<PositionComponent>(entity2.Key))
                    {
                        Point entity2Pos = cm.GetComponentForEntity<PositionComponent>(entity2.Key).position.ToPoint();
                        Point correctedPos = new Point(entity2Pos.X - (collisionComponent2.collisionBox.Width / 2), entity2Pos.Y - (collisionComponent2.collisionBox.Height / 2));
                        collisionComponent2.collisionBox.Location = correctedPos;
                        if (rectToCheck.Intersects(collisionComponent2.collisionBox))
                        {
                            //Collision detected
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
