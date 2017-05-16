using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameEngine
{
    class RenderCollisionBoxSystem : ISystem, IRenderSystem
    {
        void ISystem.Update(GameTime gameTime) {}

        public void Render(RenderHelper rh)
        {
            GraphicsDevice graphicsDevice = rh.graphicsDevice;
            SpriteBatch spriteBatch = rh.spriteBatch;
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(graphicsDevice);
            foreach (var entity in cm.GetComponentsOfType<CollisionComponent>())
            {
                CollisionComponent collisionComponent = (CollisionComponent)entity.Value;
                spriteBatch.DrawRectangle(collisionComponent.collisionBox.WorldToScreen(ref viewport), 2, Color.Black);
            }
        }
    }
}
