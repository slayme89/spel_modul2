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
            Rectangle bounds = viewport.Bounds;

            spriteBatch.Begin();

            foreach (var entity in cm.GetComponentsOfType<TextureComponent>())
            {
                TextureComponent texture = (TextureComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                
                if(positionComponent != null)
                {
                    Point position = positionComponent.position - texture.offset;

                    if (bounds.Contains(position))
                        spriteBatch.Draw(texture.texture, position.WorldToScreen(ref viewport).ToVector2(), Color.White);
                }
            }

            foreach (var entity in cm.GetComponentsOfType<AnimationComponent>())
            {
                AnimationComponent animation = (AnimationComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position - animation.offset;

                    if (bounds.Contains(position))
                        spriteBatch.Draw(animation.spriteSheet, position.WorldToScreen(ref viewport).ToVector2(), animation.sourceRectangle, Color.White);
                }
            }

            spriteBatch.End();
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

    public static class Extensions
    {
        public static Point ScreenToWorld(this Point point, ref Viewport viewport)
        {
            throw new NotImplementedException();
        }

        public static Rectangle ScreenToWorld(this Rectangle rectangle, ref Viewport viewport)
        {
            throw new NotImplementedException();
        }

        public static Point WorldToScreen(this Point point, ref Viewport viewport)
        {
            point.X += viewport.Width / 2 - viewport.Bounds.Center.X;
            point.Y += viewport.Height / 2 - viewport.Bounds.Center.Y;
            return point;
        }

        public static Rectangle WorldToScreen(this Rectangle rectangle, ref Viewport viewport)
        {
            rectangle.Offset(viewport.Width / 2 - viewport.Bounds.Center.X, viewport.Height / 2 - viewport.Bounds.Center.Y);
            return rectangle;
        }
    }
}

interface IRenderSystem
{
    void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch);
}