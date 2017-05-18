using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class CollisionComponent : IComponent
    {
        public Rectangle collisionBox;
        public CollisionComponent(int height, int width)
        {
            collisionBox.Height = height;
            collisionBox.Width = width;
        }
    }
}
