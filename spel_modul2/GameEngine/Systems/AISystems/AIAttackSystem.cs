using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameEngine.Components;

namespace GameEngine.Systems
{
    class AIAttackSystem : ISystem
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
                        float dist = Vector2.Distance(players[i].Item2.position, posComp.position);
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
                        Vector2 pointToCompare = posComp.position + new Vector2(moveComp.Direction.X * 5, moveComp.Direction.Y * 5);
                        if (attackComponent.AttackCooldown <= 0.0f && Vector2.Distance(cm.GetComponentForEntity<PositionComponent>(closestEntity).position, pointToCompare) <= 70)
                        {
                            PositionComponent posOftarget = cm.GetComponentForEntity<PositionComponent>(ai.TargetEntity);
                            Vector2 unNormalizedDir = new Vector2(posOftarget.position.X - posComp.position.X, posOftarget.position.Y - posComp.position.Y);
                            float distance = (float)Math.Sqrt(unNormalizedDir.X * unNormalizedDir.X + unNormalizedDir.Y * unNormalizedDir.Y);
                            Vector2 direction = new Vector2(unNormalizedDir.X / distance, unNormalizedDir.Y / distance);
                            moveComp.Direction = MoveSystem.CalcDirection(direction.X, direction.Y);
                            moveComp.canMove = false;
                            attackComponent.AttackCooldown = attackComponent.RateOfFire;
                            attackComponent.IsAttacking = true;
                            cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayAttackSound = true;
                        }
                    }
                    if (attackComponent.AttackCooldown > 0.0f)
                        attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        moveComp.canMove = true;
                }
            }
        }
    }
}
