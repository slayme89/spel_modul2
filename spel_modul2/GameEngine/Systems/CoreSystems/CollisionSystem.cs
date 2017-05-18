using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using GameEngine.Components;
using GameEngine.Managers;

namespace GameEngine.Systems
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
                    r1 = c[i].Item2.CollisionBox;
                    r1.Offset(-r1.Width / 2, -r1.Height / 2);
                    r1.Offset(c[i].Item3.Position);

                    r2 = c[j].Item2.CollisionBox;
                    r2.Offset(-r2.Width / 2, -r2.Height / 2);
                    r2.Offset(c[j].Item3.Position);

                    if (r1.Intersects(r2))
                        ResolveCollision(c[i], c[j], r1, r2);
                }
            }
        }

        public static List<int> DetectAreaCollision(Rectangle area)
        {
            var cm = ComponentManager.GetInstance();
            List<int> foundEntities = new List<int>();
            foreach (var entity in cm.GetComponentsOfType<CollisionComponent>())
            {
                PositionComponent positionComponent;
                CollisionComponent collisionComponent;
                if (cm.GetComponentsForEntity(entity.Key, out positionComponent, out collisionComponent))
                {
                    Rectangle bb = collisionComponent.CollisionBox;
                    bb.Offset(positionComponent.Position.X - bb.Width / 2, positionComponent.Position.Y - bb.Height / 2);

                    if (area.Intersects(bb))
                    {
                        //Collision detected, add them to the list
                        foundEntities.Add(entity.Key);
                    }
                }
            }
            return foundEntities;
        }

        private void ResolveCollision(Tuple<int, CollisionComponent, PositionComponent> e1, Tuple<int, CollisionComponent, PositionComponent> e2, Rectangle r1, Rectangle r2)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            MoveComponent m1 = cm.GetComponentForEntity<MoveComponent>(e1.Item1);
            MoveComponent m2 = cm.GetComponentForEntity<MoveComponent>(e2.Item1);
            Rectangle r3 = Rectangle.Intersect(r1, r2);

            Point axis = GetCollisionAxis(r1, r2);
            if (m1 != null && m2 != null)
            {
                e1.Item3.Position += -m1.Velocity * axis.ToVector2();
            }
            else if (m1 != null)
            {
                e1.Item3.Position += -m1.Velocity * axis.ToVector2();
            }
            else if (m2 != null)
            {

                e2.Item3.Position += -m2.Velocity * axis.ToVector2();
            }
        }

        private Point GetCollisionAxis(Rectangle r1, Rectangle r2)
        {
            if (r1.Intersects(new Rectangle(r2.Left, r2.Top + 2, 1, r2.Height - 4)))
                return new Point(1, 0);
            else if (r1.Intersects(new Rectangle(r2.Right - 1, r2.Top + 2, 1, r2.Height - 4)))
                return new Point(1, 0);
            else if (r1.Intersects(new Rectangle(r2.Left + 2, r2.Top, r2.Width - 4, 1)))
                return new Point(0, 1);
            else if (r1.Intersects(new Rectangle(r2.Left + 2, r2.Bottom - 1, r2.Width - 4, 1)))
                return new Point(0, 1);
            return new Point();
        }
    }
}
