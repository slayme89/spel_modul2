using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    }
}