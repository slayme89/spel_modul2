using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class CollisionComponent : IComponent
    {
        public Rectangle CollisionBox;
        public Point Offset;

        public CollisionComponent(int width, int height)
        {
            CollisionBox.Width = width;
            CollisionBox.Height = height;
            Offset = new Point(0, 0);
        }

        public CollisionComponent(Point size, Point offset)
        {
            CollisionBox.Width = size.X;
            CollisionBox.Height = size.Y;
            Offset = offset;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
