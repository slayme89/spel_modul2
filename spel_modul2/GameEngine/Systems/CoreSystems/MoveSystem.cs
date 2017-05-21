using GameEngine.Components;
using System;
using Microsoft.Xna.Framework;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    public class MoveSystem : ISystem
    {
        private Group<MoveComponent, PositionComponent> movements;

        public MoveSystem()
        {
            movements = new Group<MoveComponent, PositionComponent>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in movements)
            {
                MoveComponent moveComponent = entity.Item1;
                PositionComponent positionComponent = entity.Item2;
                Vector2 velocity = moveComponent.Velocity;

                float x = moveComponent.Velocity.X;
                float y = moveComponent.Velocity.Y;

                ApplyMovement(x, y, moveComponent.Speed, (float)gameTime.ElapsedGameTime.TotalMilliseconds, positionComponent, entity.Entity);
                // Check for direction
                if (moveComponent.Velocity != new Vector2(0.0f, 0.0f))
                    moveComponent.Direction = CalcDirection(x, y);
            }
        }

        private void ApplyMovement(float x, float y, float speed, float elapsedSeconds, PositionComponent pos, int entity)
        {
            x *= elapsedSeconds * speed;
            y *= elapsedSeconds * speed;

            pos.Position += new Vector2(x, y);
            
            /*for (float i = 1; i > 0.1; i *= 0.8f)
            {
                x *= i;
                y *= i;
                Vector2 futurePosition = pos.position + new Vector2(x, y);
                if (!SystemManager.GetInstance().GetSystem<CollisionSystem>().DetectMovementCollision(entity, futurePosition))
                {
                    pos.position = futurePosition;
                    break;
                }
            }*/
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
