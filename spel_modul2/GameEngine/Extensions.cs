using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GameEngine
{
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

        public static Viewport GetCurrentViewport(GraphicsDevice graphicsDevice)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;
            Rectangle bounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            bounds.Offset(world.center - new Point(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2));

            return new Viewport(bounds);
        }

        static Texture2D rectangleTexture;

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, int strokeWidth, Color color)
        {
            if (rectangleTexture == null)
            {
                rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                rectangleTexture.SetData(new[] { Color.White });
            }

            spriteBatch.Draw(rectangleTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, strokeWidth), color);
            spriteBatch.Draw(rectangleTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width + strokeWidth, strokeWidth), color);
            spriteBatch.Draw(rectangleTexture, new Rectangle(rectangle.Left, rectangle.Top, strokeWidth, rectangle.Height), color);
            spriteBatch.Draw(rectangleTexture, new Rectangle(rectangle.Right, rectangle.Top, strokeWidth, rectangle.Height), color);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, RenderLayer layer)
        {
            //spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, float layerDepth);
        }
    }
}