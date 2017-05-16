using System;
using Microsoft.Xna.Framework;

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
                moveComponent.Direction = CalcDirection(x, y);


                //if (cm.HasEntityComponent<KnockbackComponent>(entity.Key))
                //{
                //    KnockbackComponent knockbackComponent = cm.GetComponentForEntity<KnockbackComponent>(entity.Key);
                //    if (knockbackComponent.KnockbackActive)
                //    {
                //        x = knockbackComponent.KnockbackDir.X;
                //        y = knockbackComponent.KnockbackDir.Y;
                //        knockbackComponent.Cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                //        if (knockbackComponent.Cooldown <= 0.0f)
                //        {
                //            knockbackComponent.KnockbackActive = false;
                //        }
                //        ApplyMovement(x, y, moveComponent.Speed, (float)gameTime.ElapsedGameTime.TotalSeconds, positionComponent, entity.Key);
                //        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                //    }
                //    else if (moveComponent.canMove)
                //    {
                //        x = velocity.X;
                //        y = velocity.Y;
                //        // Check for direction
                //        moveComponent.Direction = CalcDirection(x, y);
                //        ApplyMovement(x, y, moveComponent.Speed, (float)gameTime.ElapsedGameTime.TotalSeconds, positionComponent, entity.Key);
                //        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = true;
                //    }
                //    else
                //    {
                //        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                //    }
                //}
                //else if(moveComponent.canMove)
                //{
                //    x = velocity.X;
                //    y = velocity.Y;
                //    // Check for direction
                //    moveComponent.Direction = CalcDirection(x, y);
                //    ApplyMovement(x, y, moveComponent.Speed, (float)gameTime.ElapsedGameTime.TotalSeconds, positionComponent, entity.Key);
                //    cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = true;
                //}
                //else
                //{
                //    cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                //}
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
