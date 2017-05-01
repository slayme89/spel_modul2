using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    enum RenderLayer { Layer1, Layer2, Layer3, Layer4 };

    class RenderSystem : ISystem, IRenderSystem
    {
        void ISystem.Update(GameTime gameTime) {}

        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            spriteBatch.Begin();

            //TODO: Calculate the viewport rectangle in world coordinates.
            //      Group all entities with a position component by tileId.
            //      Get all tiles which bounds intersect the viewport rectangle OR
            //      store a bound rectangle and iterate over them and do an intersect test. //Probably easier for a small game.
            //      
            //      
            //      
            //Rectangle r = new Rectangle();

            foreach (var entity in cm.GetComponentsOfType<TextureComponent>())
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

            Viewport hej = graphicsDevice.Viewport;

            spriteBatch.End();
        }
    }
}

interface IRenderSystem
{
    void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch);
}