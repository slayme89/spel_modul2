using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine
{
    class MoveSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<MoveComponent>())
            {
                MoveComponent moveComponent = (MoveComponent)entity.Value;
                Vector2 velocity = moveComponent.Velocity;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                float x = moveComponent.Velocity.X;
                float y = moveComponent.Velocity.Y;

                ApplyMovement(x, y, moveComponent.Speed, (float)gameTime.ElapsedGameTime.TotalMilliseconds, positionComponent, entity.Key);
                // Check for direction
                if(moveComponent.Velocity != new Vector2(0.0f, 0.0f))
                    moveComponent.Direction = CalcDirection(x, y);
                
            }
        }

        private void ApplyMovement(float x, float y, float speed, float elapsedSeconds, PositionComponent pos, int entity)
        {
            x *= elapsedSeconds * speed;
            y *= elapsedSeconds * speed;
            
            for (float i = 1; i > 0.1; i *= 0.8f)
            {
                x *= i;
                y *= i;
                Vector2 futurePosition = pos.position + new Vector2(x, y);
                if (!SystemManager.GetInstance().GetSystem<CollisionSystem>().DetectMovementCollision(entity, futurePosition))
                {
                    pos.position = futurePosition;
                    break;
                }
            }
        }

        public static Point CalcDirection(float x, float y)
        {
            if (x > 0)
            {
                if (y > 0)
                {
                    if (x > y)
                        return new Point(1, 0);
                    else
                        return new Point(0, 1);
                }
                else
                {
                    if (x > -y)
                        return new Point(1, 0);
                    else
                        return new Point(0, -1);
                }
            }
            else
            {
                if (y > 0)
                {
                    if (-x > y)
                        return new Point(-1, 0);
                    else
                        return new Point(0, 1);
                }
                else
                {
                    if (-x > -y)
                        return new Point(-1, 0);
                    else
                        return new Point(0, -1);
                }
            }
        }
    }
}
