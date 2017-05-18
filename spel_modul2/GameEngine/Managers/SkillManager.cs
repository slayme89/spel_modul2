using GameEngine.Components;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Managers
{
    public class SkillManager
    {
        public static void HeavyAttack(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity);
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity);
            int width = 40;
            int height = 40;
            Point attackPoint = new Point(moveComp.Direction.X * (width/2), moveComp.Direction.Y * (height/2));
            Rectangle areOfEffect = new Rectangle(posComp.Position.ToPoint() + attackPoint, new Point(width, height));
            List<int> entitiesHit = CollisionSystem.DetectAreaCollision(areOfEffect);
            foreach(int entityHit in entitiesHit)
            {
                if (entityHit == entity)
                    continue;
                if (cm.HasEntityComponent<DamageComponent>(entityHit) && !cm.HasEntityComponent<PlayerControlComponent>(entityHit))
                {
                    DamageComponent damageComp = cm.GetComponentForEntity<DamageComponent>(entityHit);
                    damageComp.IncomingDamage.Add(100);
                    damageComp.LastAttacker = entity;
                }
            }
        }
    }
}
