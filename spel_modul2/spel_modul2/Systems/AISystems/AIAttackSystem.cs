using System;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AIAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach(var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                if(cm.HasEntityComponent<AIComponent>(entity.Key) && cm.HasEntityComponent<PositionComponent>(entity.Key))
                {
                    AIComponent ai = cm.GetComponentForEntity<AIComponent>(entity.Key);
                    PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                    Rectangle detectArea = new Rectangle(new Vector2(posComp.position.X - (ai.DetectRange.X / 2), posComp.position.Y - (ai.DetectRange.Y / 2)).ToPoint(), ai.DetectRange);

                    //find closest target
                    int closestEntity = 0;
                    float closestDist = 0;

                    // Can this be changed? (not calling methoth from other system) 
                    /*foreach (int entityFound in CollisionSystem.DetectAreaCollision(detectArea))
                    {
                        if (entityFound == entity.Key || !cm.HasEntityComponent<HealthComponent>(entityFound) && !cm.HasEntityComponent<PlayerControlComponent>(entityFound))
                            continue;
                        if (closestEntity == 0)
                        {
                            closestEntity = entityFound;
                            closestDist = Vector2.Distance(posComp.position, cm.GetComponentForEntity<PositionComponent>(closestEntity).position);
                            continue;
                        }
                        PositionComponent closestEntPos = cm.GetComponentForEntity<PositionComponent>(closestEntity);
                        PositionComponent entToCheckPos = cm.GetComponentForEntity<PositionComponent>(entityFound);
                        float distToCheck = Vector2.Distance(posComp.position, entToCheckPos.position);
                        if (distToCheck < closestDist)
                        {
                            closestEntity = entityFound;
                            closestDist = distToCheck;
                        }
                    }*/

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
