using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class RenderSystem : ISystem, IRenderSystem
    {
        void ISystem.Update(GameTime gameTime) {}

        public void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            spriteBatch.Begin();

            foreach(var entity in cm.GetComponentsOfType<TextureComponent>())
            {
                TextureComponent texture = (TextureComponent)entity.Value;
                PositionComponent position = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                
                if(position != null)
                {
                    spriteBatch.Draw(texture.texture, (position.position - texture.offset).ToVector2(), Color.White);
                }
            }

            foreach (var entity in cm.GetComponentsOfType<AnimationComponent>())
            {
                AnimationComponent animation = (AnimationComponent)entity.Value;
                PositionComponent position = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (position != null)
                {
                    spriteBatch.Draw(animation.spriteSheet, (position.position - animation.offset).ToVector2(), animation.sourceRectangle, Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}

interface IRenderSystem
{
    void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch);
}