using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{

    // This is for debugging attacks
    class RenderAttackingCollisionBoxSystem : ISystem, IRenderSystem
    {
        private Texture2D t;

        public RenderAttackingCollisionBoxSystem(GraphicsDevice gd)
        {
            t = new Texture2D(gd, 1, 1);
            t.SetData(new[] { Color.White });
        }

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
                        CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(Entity.Key);
                        int range = collisionComponent.collisionBox.Size.X;
                        Point hitOffset = new Point((collisionComponent.collisionBox.Width / 2), (collisionComponent.collisionBox.Height / 2));
                        Rectangle hitArea = new Rectangle(positionComponent.position.ToPoint() - hitOffset + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size).WorldToScreen(ref viewport);
                        if(attackComponent.IsAttacking)
                            rh.Draw(t, hitArea, Color.Black, RenderLayer.Foreground1);
                        else
                            rh.Draw(t, hitArea, Color.Green, RenderLayer.Foreground1);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
