using Game.Components;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    // This is for debugging attacks
    public class RenderAttackingCollisionBoxSystem : ISystem, IRenderSystem
    {
        public void Render(RenderHelper rh)
        {
            GraphicsDevice gd = rh.graphicsDevice;
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(gd);
            foreach (var Entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AttackComponent attackComponent = (AttackComponent)Entity.Value;
                if (attackComponent.AttackCooldown > 0.0f)
                {
                    MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(Entity.Key);
                    PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(Entity.Key);
                    if (attackComponent.Type == WeaponType.Sword)
                    {
                        int range = attackComponent.AttackCollisionBox.Size.X;
                        Point hitOffset = new Point((attackComponent.AttackCollisionBox.Width / 2), (attackComponent.AttackCollisionBox.Height / 2));
                        Rectangle hitArea = new Rectangle(positionComponent.Position.ToPoint() - hitOffset + moveComponent.Direction * new Point(range, range), attackComponent.AttackCollisionBox.Size).WorldToScreen(ref viewport);
                        if(attackComponent.IsAttacking)
                            rh.DrawFilledRectangle(hitArea, Color.Red, RenderLayer.Foreground1);
                        else
                            rh.DrawFilledRectangle(hitArea, Color.Red, RenderLayer.Foreground1);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
