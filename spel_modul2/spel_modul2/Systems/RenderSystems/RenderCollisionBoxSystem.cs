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
    class RenderCollisionBoxSystem : ISystem
    {
        private Texture2D t;

        public RenderCollisionBoxSystem(GraphicsDevice gd)
        {
            t = new Texture2D(gd, 1, 1);
            t.SetData(new[] { Color.White });
        }
        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<CollisionComponent>())
            {
                CollisionComponent collisionComponent = (CollisionComponent)entity.Value;
                spriteBatch.Begin();
                spriteBatch.Draw(t, new Rectangle(collisionComponent.collisionBox.Left, collisionComponent.collisionBox.Top, 2, collisionComponent.collisionBox.Height), Color.Black); // Left
                spriteBatch.Draw(t, new Rectangle(collisionComponent.collisionBox.Right, collisionComponent.collisionBox.Top, 2, collisionComponent.collisionBox.Height), Color.Black); // Right
                spriteBatch.Draw(t, new Rectangle(collisionComponent.collisionBox.Left, collisionComponent.collisionBox.Top, collisionComponent.collisionBox.Width, 2), Color.Black); // Top
                spriteBatch.Draw(t, new Rectangle(collisionComponent.collisionBox.Left, collisionComponent.collisionBox.Bottom, collisionComponent.collisionBox.Width, 2), Color.Black); // Bottom
                spriteBatch.End();
            }
        }
    }
}
