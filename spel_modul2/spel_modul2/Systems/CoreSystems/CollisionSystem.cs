using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace GameEngine
{
    class CollisionSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            List<Tuple<int, CollisionComponent, PositionComponent>> c = new List<Tuple<int, CollisionComponent, PositionComponent>>();

            Dictionary<int, IComponent> entities = cm.GetComponentsOfType<CollisionComponent>();
            foreach (var entity in entities.Keys)
            {
                CollisionComponent collisionComponent;
                PositionComponent positionComponent;

                cm.GetComponentsForEntity(entity, out collisionComponent, out positionComponent);
                c.Add(new Tuple<int, CollisionComponent, PositionComponent>(entity, collisionComponent, positionComponent));
            }

            for (int i = 0; i < c.Count; i++)
            {
                for (int j = i + 1; j < c.Count; j++)
                {
                    Rectangle r1, r2;
                    r1 = c[i].Item2.collisionBox;
                    r1.Offset(-r1.Width / 2, -r1.Height / 2);
                    r1.Offset(c[i].Item3.position);

                    r2 = c[j].Item2.collisionBox;
                    r2.Offset(-r2.Width / 2, -r2.Height / 2);
                    r2.Offset(c[j].Item3.position);

                    if (r1.Intersects(r2))
                        ResolveCollision(c[i], c[j]);
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

        private void ResolveCollision(Tuple<int, CollisionComponent, PositionComponent> e1, Tuple<int, CollisionComponent, PositionComponent> e2)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            MoveComponent m1 = cm.GetComponentForEntity<MoveComponent>(e1.Item1);
            MoveComponent m2 = cm.GetComponentForEntity<MoveComponent>(e2.Item1);

            if(m1 != null && m2 != null)
            {
                e1.Item3.position += -m1.Direction.ToVector2() * m1.Speed;
            }
            else if(m1 != null)
            {
                e1.Item3.position += -m1.Direction.ToVector2() * m1.Speed;
            }
            else if(m2 != null)
            {
                e2.Item3.position += -m2.Direction.ToVector2() * m2.Speed;
            }

            e1.Item2.onHit?.Invoke(e2.Item1);
            e2.Item2.onHit?.Invoke(e1.Item1);
        }
    }
}
