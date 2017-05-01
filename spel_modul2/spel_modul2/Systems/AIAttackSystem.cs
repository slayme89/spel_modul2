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

                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                    if (ai.TargetEntity != 0)
                    {
                        if (attackComponent.AttackCooldown <= 0.0f && closestDist <= 100)
                        {
                            moveComponent.canMove = false;
                            attackComponent.AttackCooldown = attackComponent.RateOfFire;
                            attackComponent.IsAttacking = true;
                        }
                    }
                    if (attackComponent.AttackCooldown > 0.0f)
                        attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        moveComponent.canMove = true;
                }
            }
        }
    }
}
