using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using GameEngine.Components;
using GameEngine.Managers;
using System.Diagnostics;

namespace GameEngine.Systems
{
    public class CollisionSystem : ISystem
    {
        private Group<CollisionComponent, PositionComponent> collisions;

        public CollisionSystem()
        {
            collisions = new Group<CollisionComponent, PositionComponent>();
        }

        public void Update(GameTime gameTime)
        {
            List<Tuple<int, CollisionComponent, PositionComponent>> c = new List<Tuple<int, CollisionComponent, PositionComponent>>();
            
            foreach (var entity in collisions)
            {
                CollisionComponent collisionComponent = entity.Item1;
                PositionComponent positionComponent = entity.Item2;
                
                c.Add(new Tuple<int, CollisionComponent, PositionComponent>(entity.Entity, collisionComponent, positionComponent));
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
                if (cm.TryGetEntityComponents(entity.Key, out positionComponent, out collisionComponent))
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
            float x = 0, y = 0;

            Vector2 dir = e1.Item3.Position - e2.Item3.Position;

            Point axis;
            if (m1 != null && m2 != null)
            {
                axis = GetCollisionAxis(r1, r2, r3);

                Vector2 dist = (r3.Size.ToVector2() * axis.ToVector2());
                e1.Item3.Position += dist;
            }
            else if (m1 != null)
            {
                axis = GetCollisionAxis(r1, r2, r3);

                Vector2 dist = (r3.Size.ToVector2() * axis.ToVector2());
                e1.Item3.Position += dist;
            }
            else if (m2 != null)
            {
                axis = GetCollisionAxis(r2, r1, r3);

                Vector2 dist = (r3.Size.ToVector2() * axis.ToVector2());
                e2.Item3.Position += dist;
            }


            //Point axis;
            //if (m1 != null && m2 != null)
            //{
            //    axis = GetCollisionAxis(r1, r2);
            //    e1.Item3.Position += -m1.Velocity * axis.ToVector2();
            //}
            //else if (m1 != null)
            //{
            //    axis = GetCollisionAxis(r1, r2);
            //    e1.Item3.Position += -m1.Velocity * axis.ToVector2();
            //}
            //else if (m2 != null)
            //{
            //    axis = GetCollisionAxis(r2, r1);
            //    e2.Item3.Position += -m2.Velocity * axis.ToVector2();
            //}
        }
        //r3 is intersecting rectangle between r1 and r2
        private Point GetCollisionAxis(Rectangle r1, Rectangle r2, Rectangle r3)
        {
            //Top Wall
            if(r1.Bottom >= r2.Top && r1.Top < r2.Top && r3.Width > r3.Height)
                return new Point(0, -1);
            //Left Wall
            if(r1.Right >= r2.Left && r1.Left < r2.Left && r3.Height > r3.Width)
                return new Point(-1, 0);
            //Bottom Wall
            if(r1.Top <= r2.Bottom && r1.Bottom > r2.Bottom && r3.Width > r3.Height)
                return new Point(0, 1);
            //Right Wall
            if(r1.Left <= r2.Right && r1.Right > r2.Right && r3.Height > r3.Width)
                return new Point(1, 0);
            return new Point(1, 1);
        }

        public static Point CalcDirection(float x, float y)
        {
            if (Math.Abs(x) > Math.Abs(y))
                return x > 0 ? new Point(1, 0) : new Point(-1, 0);
            else
                return y > 0 ? new Point(0, 1) : new Point(0, -1);
        }
    }
}
