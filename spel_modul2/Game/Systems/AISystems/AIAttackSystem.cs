using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Game.Components;
using System.Diagnostics;

namespace Game.Systems
{
    public class AIAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            List<Tuple<int, PositionComponent>> players = new List<Tuple<int, PositionComponent>>();
            foreach(var player in cm.GetComponentsOfType<PlayerComponent>())
            {
                players.Add(new Tuple<int, PositionComponent>(player.Key, cm.GetComponentForEntity<PositionComponent>(player.Key)));
            }

            foreach (var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                if(cm.HasEntityComponent<AIComponent>(entity.Key) && cm.HasEntityComponent<PositionComponent>(entity.Key))
                {
                    AIComponent ai = cm.GetComponentForEntity<AIComponent>(entity.Key);
                    PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                    //find closest target
                    int closestEntity = 0;
                    float closestDist = float.MaxValue;

                    for(int i = 0; i < players.Count; i++)
                    {
                        float dist = Vector2.Distance(players[i].Item2.Position, posComp.Position);
                        if (dist < ai.DetectRange && dist < closestDist)
                        {
                            closestEntity = players[i].Item1;
                        }
                    }

                    if (closestEntity != 0)
                        ai.TargetEntity = closestEntity;
                    else
                        ai.TargetEntity = 0;

                    // Do some check here: cm.HasEntity....<movecomp> / <attackcomp> ?
                    MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    if (ai.TargetEntity != 0)
                    {
                        Vector2 pointToTarget, pointToTCompare;
                        Point nextDir;
                        PositionComponent posOftarget = cm.GetComponentForEntity<PositionComponent>(ai.TargetEntity);
                        CollisionComponent collComp = cm.GetComponentForEntity<CollisionComponent>(entity.Key);
                        Vector2 unNormalizedDir = new Vector2(posOftarget.Position.X - posComp.Position.X, posOftarget.Position.Y - posComp.Position.Y);
                        float distance = (float)Math.Sqrt(unNormalizedDir.X * unNormalizedDir.X + unNormalizedDir.Y * unNormalizedDir.Y);
                        Vector2 direction = new Vector2(unNormalizedDir.X / distance, unNormalizedDir.Y / distance);

                        nextDir = MoveSystem.CalcDirection(direction.X, direction.Y);
                        pointToTarget = posOftarget.Position + new Vector2(-nextDir.X * (collComp.CollisionBox.Width), -nextDir.Y * (collComp.CollisionBox.Height));
                        pointToTCompare = posComp.Position + new Vector2(nextDir.X * (collComp.CollisionBox.Width), nextDir.Y * (collComp.CollisionBox.Height));
                        ai.Destination = pointToTarget.ToPoint();

                        if (attackComponent.AttackCooldown <= 0.0f
                            && Vector2.Distance(posComp.Position, pointToTarget) <= (collComp.CollisionBox.Width * Math.Abs(nextDir.X) + collComp.CollisionBox.Height * Math.Abs(nextDir.Y)) / 2
                            && !cm.GetComponentForEntity<KnockbackComponent>(entity.Key).KnockbackActive)
                        {
                            moveComp.Direction = nextDir;
                            moveComp.CanMove = false;
                            attackComponent.AttackCooldown = attackComponent.RateOfFire;
                            attackComponent.IsAttacking = true;
                            cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayAttackSound = true;
                            if (cm.HasEntityComponent<AnimationGroupComponent>(entity.Key))
                            {
                                AnimationGroupComponent animGroupComp = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key);
                                int anim = GetAnimationRow(moveComp.Direction) + 8;
                                if (animGroupComp.ActiveAnimation != anim)
                                    animGroupComp.ActiveAnimation = anim;
                            }
                        }

                    }
                    if (attackComponent.AttackCooldown > 0.0f)
                        attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        moveComp.CanMove = true;
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
