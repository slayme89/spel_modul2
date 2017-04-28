using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MoveComponent : IComponent
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public Point Direction { get; set; }
        public bool canMove { get; set; }
        public MoveComponent()
        {
            canMove = true;
            Velocity = new Vector2();
            Speed = 1.0f;
            Direction = new Point(0, 1);
        }

        public MoveComponent(float speed)
        {
            canMove = true;
            Velocity = new Vector2();
            Speed = speed;
            Direction = new Point(0, 1);
        }
    }
}
