using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class CollisionComponent : IComponent
    {
        public Rectangle collisionBox;
        public CollisionComponent(int width, int height)
        {
            collisionBox.Height = height;
            collisionBox.Width = width;
        }
    }
}
