using Microsoft.Xna.Framework;

namespace GameEngine
{
    class CollisionComponent : IComponent
    {
        public Rectangle collisionBox;
        public bool checkAttackCollision { get; set; }

        public CollisionComponent(int height, int width)
        {
            collisionBox.Height = height;
            collisionBox.Width = width;
            checkAttackCollision = false;
        }
    }
}
