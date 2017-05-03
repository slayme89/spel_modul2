using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{

    // This is for debugging attacks
    class RenderAttackingCollisionBoxSystem : ISystem
    {
        private Texture2D t;

        public RenderAttackingCollisionBoxSystem(GraphicsDevice gd)
        {
            t = new Texture2D(gd, 1, 1);
            t.SetData(new[] { Color.White });
        }

        public void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(graphicsDeive);
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
                        Rectangle hitArea = new Rectangle(positionComponent.position - hitOffset + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size).WorldToScreen(ref viewport);
                        if(attackComponent.IsAttacking)
                            spriteBatch.Draw(t, hitArea, Color.Black);
                        else
                            spriteBatch.Draw(t, hitArea, Color.Green);
                    }
                }
            }
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
