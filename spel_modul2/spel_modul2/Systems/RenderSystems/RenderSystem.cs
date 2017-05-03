using System;
using System.Linq;
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
            Viewport viewport = GetCurrentViewport(graphicsDevice);
            Rectangle viewportBounds = viewport.Bounds;

            spriteBatch.Begin();

            //Render all textures
            foreach (var entity in cm.GetComponentsOfType<TextureComponent>())
            {
                TextureComponent textureComponent = (TextureComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position - textureComponent.offset;
                    Rectangle textureBounds = new Rectangle(position.X, position.Y, textureComponent.texture.Width, textureComponent.texture.Height);

                    if (viewportBounds.Intersects(textureBounds))
                        spriteBatch.Draw(textureComponent.texture, position.WorldToScreen(ref viewport).ToVector2(), Color.White);
                }
            }

            //Render all animations
            foreach (var entity in cm.GetComponentsOfType<AnimationComponent>())
            {
                AnimationComponent animationComponent = (AnimationComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position - animationComponent.offset;
                    Rectangle animationBounds = new Rectangle(position.X, position.Y, animationComponent.frameSize.X, animationComponent.frameSize.Y);

                    if (viewportBounds.Intersects(animationBounds))
                        spriteBatch.Draw(animationComponent.spriteSheet, position.WorldToScreen(ref viewport).ToVector2(), animationComponent.sourceRectangle, Color.White);
                }
            }

            RenderTiles(graphicsDevice, spriteBatch);

            spriteBatch.End();
        }

        void RenderTiles(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {

        }

        private Viewport GetCurrentViewport(GraphicsDevice graphicsDevice)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;
            Rectangle bounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            bounds.Offset(world.center - new Point(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2));

            return new Viewport(bounds);
        }
    }
}

interface IRenderSystem
{
    void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch);
}