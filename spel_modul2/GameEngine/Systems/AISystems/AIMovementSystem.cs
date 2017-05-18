using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Systems
{
    class AIMovementSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            var cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<AIComponent>())
            {
                if (cm.HasEntityComponent<MoveComponent>(entity.Key))
                {
                    MoveComponent moveComp = ComponentManager.GetInstance().GetComponentForEntity<MoveComponent>(entity.Key);
                    if (((AIComponent)entity.Value).TargetEntity != 0)
                    {
                        if (cm.HasEntityComponent<PositionComponent>(entity.Key) && moveComp.CanMove)
                        {
                            PositionComponent posComp = ComponentManager.GetInstance().GetComponentForEntity<PositionComponent>(entity.Key);
                            ((AIComponent)entity.Value).Destination = cm.GetComponentForEntity<PositionComponent>(((AIComponent)entity.Value).TargetEntity).Position.ToPoint();
                            Vector2 pointToCompare = posComp.Position + new Vector2(moveComp.Direction.X * 5, moveComp.Direction.Y * 5);
                            Vector2 nextMovement = new Vector2(((AIComponent)entity.Value).Destination.X - pointToCompare.X, ((AIComponent)entity.Value).Destination.Y - pointToCompare.Y);
                            float distance = (float)Math.Sqrt(nextMovement.X * nextMovement.X + nextMovement.Y * nextMovement.Y);

                            if (distance > 2f)
                            {
                                moveComp.Velocity = new Vector2(nextMovement.X / distance, nextMovement.Y / distance);
                                cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = true;
                            }

                            else
                            {
                                moveComp.Velocity = new Vector2(0, 0);
                                cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                            }

                        }
                    }
                    else
                    {
                        moveComp.Velocity = new Vector2(0, 0);
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                    }
                }
            }
        }
    }
}
