using Microsoft.Xna.Framework;

namespace GameEngine
{
    class CollisionComponent : IComponent
    {
        public Rectangle collisionBox;
        public Rectangle attackCollisionBox;
        public bool checkAttackColision { get; set; }

        public CollisionComponent(int height, int width)
        {
            collisionBox.Height = height;
            collisionBox.Width = width;
            attackCollisionBox = new Rectangle();
            checkAttackColision = false;
        }
    }
}
