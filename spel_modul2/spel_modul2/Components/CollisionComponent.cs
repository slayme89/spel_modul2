using Microsoft.Xna.Framework;

namespace GameEngine
{
    class CollisionComponent
    {
        public Rectangle collisionBox;

        public CollisionComponent(int height, int width, Point position)
        {
            collisionBox.Height = height;
            collisionBox.Width = width;
            collisionBox.Location = position;
        }
    }
}
