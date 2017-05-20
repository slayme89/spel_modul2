using Game.Components;
using GameEngine.Components;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Managers
{
    public class SkillManager : IRenderSystem
    {
        static Rectangle heavyAttack;
        public static void HeavyAttack(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity);
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity);
            CollisionComponent collComp = cm.GetComponentForEntity<CollisionComponent>(entity);
            StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity);
            int width = 70;
            int height = 70;
            Point attackPoint = new Point(moveComp.Direction.X * ((width/2) + (collComp.CollisionBox.Size.X/2)) - (width/2), moveComp.Direction.Y * ((height/2) + (collComp.CollisionBox.Size.Y/2)) - (height/2));
            Rectangle areOfEffect = new Rectangle(posComp.Position.ToPoint() + attackPoint , new Point(width, height));
            heavyAttack = areOfEffect;
            List<int> entitiesHit = CollisionSystem.DetectAreaCollision(areOfEffect);
            foreach(int entityHit in entitiesHit)
            {
                if (entityHit == entity)
                    continue;
                if (cm.HasEntityComponent<DamageComponent>(entityHit) && !cm.HasEntityComponent<PlayerControlComponent>(entityHit))
                {
                    DamageComponent damageComp = cm.GetComponentForEntity<DamageComponent>(entityHit);
                    float damage = 11 + statComp.Strength * 3.5f;
                    damageComp.IncomingDamage.Add((int)damage);
                    damageComp.LastAttacker = entity;
                }
            }
        }

        public void Render(RenderHelper renderHelper)
        {
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            if (heavyAttack != null)
                renderHelper.DrawRectangle(heavyAttack.WorldToScreen(ref viewport), 5, Color.Red, RenderLayer.Foreground1);
        }
    }
}
