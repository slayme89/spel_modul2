using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;

namespace Game.Systems
{
    public class AIMovementSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            var cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<AIComponent>())
            {
                if (cm.HasEntityComponent<MoveComponent>(entity.Key))
                {
                    MoveComponent moveComp = ComponentManager.GetInstance().GetComponentForEntity<MoveComponent>(entity.Key);
                    PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                    if (((AIComponent)entity.Value).TargetEntity != 0) //Vector2.Distance(((AIComponent)entity.Value).Destination.ToVector2(), posComp.Position) > 10
                    {
                        if (moveComp.CanMove)
                        {
                            Vector2 pointToCompare = posComp.Position + new Vector2(moveComp.Direction.X * 5, moveComp.Direction.Y * 5);
                            Vector2 nextMovement = new Vector2(((AIComponent)entity.Value).Destination.X - pointToCompare.X, ((AIComponent)entity.Value).Destination.Y - pointToCompare.Y);
                            float distance = (float)Math.Sqrt(nextMovement.X * nextMovement.X + nextMovement.Y * nextMovement.Y);

                            if (distance > 2f)
                            {
                                moveComp.Velocity = new Vector2(nextMovement.X / distance, nextMovement.Y / distance) * moveComp.Speed;
                                cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Play;
                                if (cm.HasEntityComponent<AnimationGroupComponent>(entity.Key))
                                {
                                    AnimationGroupComponent animGroupComp = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key);
                                    int anim = GetAnimationRow(moveComp.Direction) + 4;
                                    if (animGroupComp.ActiveAnimation != anim)
                                        animGroupComp.ActiveAnimation = anim;
                                }
                            }
                            else
                            {
                                moveComp.Velocity = new Vector2(0, 0);
                                cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Pause;
                            }
                        }
                    }
                    else
                    {
                        AnimationGroupComponent animGroupComp = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key);
                        int anim = GetAnimationRow(moveComp.Direction);
                        if (animGroupComp.ActiveAnimation != anim)
                            animGroupComp.ActiveAnimation = anim;
                        moveComp.Velocity = new Vector2(0, 0);
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Pause;
                    }
                }
            }
        }

        int GetAnimationRow(Point direction)
        {
            if (direction.X > 0 && direction.Y == 0)
                return 3;
            else if (direction.X == 0 && direction.Y > 0)
                return 0;
            else if (direction.X < 0 && direction.Y == 0)
                return 1;
            else
                return 2;
        }
    }
}
