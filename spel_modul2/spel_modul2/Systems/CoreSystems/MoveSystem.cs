using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (positionComponent == null)
                    throw new Exception("You must have a position component to be able to move an entity. Entity ID:" + entity.Key);
                if(moveComponent.canMove)
                {
                    float x = velocity.X;
                    float y = velocity.Y;
                    if (x == 0 && y == 0)
                    {
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                    }
                    else
                    {
                        // Check for direction
                        moveComponent.Direction = CalcDirection(x, y);

                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = true;
                        x *= (float)gameTime.ElapsedGameTime.TotalMilliseconds * moveComponent.Speed;
                        y *= (float)gameTime.ElapsedGameTime.TotalMilliseconds * moveComponent.Speed;

                        for (float i = 1; i > 0.1; i *= 0.8f)
                        {
                            x *= i;
                            y *= i;
                            Vector2 futurePosition = positionComponent.position + new Vector2(x, y);
                            if (!SystemManager.GetInstance().GetSystem<CollisionSystem>().DetectMovementCollision(entity.Key, futurePosition))
                            {
                                positionComponent.position = futurePosition;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                }
            }
        }

        public Point CalcDirection(float x, float y)
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
