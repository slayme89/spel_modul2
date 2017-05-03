using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    class AIMovementSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            var cm = ComponentManager.GetInstance();

            foreach (KeyValuePair<int, IComponent> pair in cm.GetComponentsOfType<AIComponent>())
            {
                MoveComponent moveComp = ComponentManager.GetInstance().GetComponentForEntity<MoveComponent>(pair.Key);
                if (((AIComponent)pair.Value).TargetEntity != 0)
                {
                    
                    if (moveComp != null)
                    {
                        PositionComponent posComp = ComponentManager.GetInstance().GetComponentForEntity<PositionComponent>(pair.Key);
                        if (posComp != null)
                        {
                            ((AIComponent)pair.Value).Destination = cm.GetComponentForEntity<PositionComponent>(((AIComponent)pair.Value).TargetEntity).position;

                            Point pointToCompare = posComp.position + new Point(moveComp.Direction.X * 5, moveComp.Direction.Y * 5);

                            Vector2 nextMovement = new Vector2(((AIComponent)pair.Value).Destination.X - pointToCompare.X, ((AIComponent)pair.Value).Destination.Y - pointToCompare.Y);
                            float distance = (float)Math.Sqrt(nextMovement.X * nextMovement.X + nextMovement.Y * nextMovement.Y);
                            if (distance > 2f)
                            {
                                Vector2 direction = new Vector2(nextMovement.X / distance, nextMovement.Y / distance);
                                moveComp.Velocity = direction;
                            }
                            else
                                moveComp.Velocity = new Vector2(0, 0);
                        }
                    }
                }else
                    moveComp.Velocity = new Vector2(0, 0);
            }
        }
    }
}
