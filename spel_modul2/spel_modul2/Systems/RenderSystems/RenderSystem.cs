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

            RenderTiles(graphicsDevice, spriteBatch);

            //Render all textures
            foreach (var entity in cm.GetComponentsOfType<TextureComponent>())
            {
                TextureComponent textureComponent = (TextureComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position.ToPoint() - textureComponent.offset;
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
                    Point position = positionComponent.position.ToPoint() - animationComponent.offset;
                    Rectangle animationBounds = new Rectangle(position.X, position.Y, animationComponent.frameSize.X, animationComponent.frameSize.Y);

                    if (viewportBounds.Intersects(animationBounds))
                        spriteBatch.Draw(animationComponent.spriteSheet, position.WorldToScreen(ref viewport).ToVector2(), animationComponent.sourceRectangle, Color.White);
                }
            }

            //Render all animationgroups
            foreach (var entity in cm.GetComponentsOfType<AnimationGroupComponent>())
            {
                AnimationGroupComponent animationComponent = (AnimationGroupComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position.ToPoint() - animationComponent.offset;
                    Rectangle animationBounds = new Rectangle(position.X, position.Y, animationComponent.frameSize.X, animationComponent.frameSize.Y);

                    if (viewportBounds.Intersects(animationBounds))
                        spriteBatch.Draw(animationComponent.spritesheet, position.WorldToScreen(ref viewport).ToVector2(), animationComponent.sourceRectangle, Color.White);
                }
            }

            spriteBatch.End();
        }

        void RenderTiles(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;
            Viewport viewport = Extensions.GetCurrentViewport(graphicsDevice);
            Rectangle viewportBounds = viewport.Bounds;

            foreach (var tile in world.tiles)
            {
                Point point = new Point(tile.Key.X * tile.Value.Width, tile.Key.Y * tile.Value.Height);
                Rectangle tileBounds = new Rectangle(point.X, point.Y, tile.Value.Width, tile.Value.Height);

                if (viewportBounds.Intersects(tileBounds))
                    spriteBatch.Draw(tile.Value, point.WorldToScreen(ref viewport).ToVector2(), Color.White);
            }
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

