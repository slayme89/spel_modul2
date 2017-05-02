using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine
{
    class AIAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AIComponent ai = cm.GetComponentForEntity<AIComponent>(entity.Key);
                if(ai != null)
                {
                    PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                    Rectangle detectArea = new Rectangle(new Point(posComp.position.X - (ai.DetectRange.X / 2), posComp.position.Y - (ai.DetectRange.Y / 2)), ai.DetectRange);

                    //find closest target
                    int closestEntity = 0;
                    float closestDist = 0;
                    foreach (int entityFound in CollisionSystem.DetectAreaCollision(detectArea))
                    {
                        if (entityFound == entity.Key || !cm.HasEntityComponent<HealthComponent>(entityFound) && !cm.HasEntityComponent<PlayerControlComponent>(entityFound))
                            continue;
                        if (closestEntity == 0)
                        {
                            closestEntity = entityFound;
                            closestDist = Vector2.Distance(posComp.position.ToVector2(), cm.GetComponentForEntity<PositionComponent>(closestEntity).position.ToVector2());
                            continue;
                        }
                        PositionComponent closestEntPos = cm.GetComponentForEntity<PositionComponent>(closestEntity);
                        PositionComponent entToCheckPos = cm.GetComponentForEntity<PositionComponent>(entityFound);
                        float distToCheck = Vector2.Distance(posComp.position.ToVector2(), entToCheckPos.position.ToVector2());
                        if (distToCheck < closestDist)
                        {
                            closestEntity = entityFound;
                            closestDist = distToCheck;
                        }
                    }

                    if (closestEntity != 0)
                        ai.TargetEntity = closestEntity;
                    else
                        ai.TargetEntity = 0;

                    MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    if (ai.TargetEntity != 0)
                    {
                        Point pointToCompare = posComp.position + new Point(moveComp.Direction.X * 5, moveComp.Direction.Y * 5);
                        if (attackComponent.AttackCooldown <= 0.0f && Vector2.Distance(cm.GetComponentForEntity<PositionComponent>(closestEntity).position.ToVector2(), pointToCompare.ToVector2()) <= 70)
                        {
                            PositionComponent posOftarget = cm.GetComponentForEntity<PositionComponent>(ai.TargetEntity);
                            Point unNormalizedDir = new Point(posOftarget.position.X - posComp.position.X, posOftarget.position.Y - posComp.position.Y);
                            float distance = (float)Math.Sqrt(unNormalizedDir.X * unNormalizedDir.X + unNormalizedDir.Y * unNormalizedDir.Y);
                            Vector2 direction = new Vector2(unNormalizedDir.X / distance, unNormalizedDir.Y / distance);
                            moveComp.Direction = SystemManager.GetInstance().GetSystem<MoveSystem>().CalcDirection(direction.X, direction.Y);
                            moveComp.canMove = false;
                            attackComponent.AttackCooldown = attackComponent.RateOfFire;
                            attackComponent.IsAttacking = true;
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
