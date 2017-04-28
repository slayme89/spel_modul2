using Microsoft.Xna.Framework;

namespace GameEngine
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    class MoveComponent : IComponent
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public Direction Direction { get; set; }
        public MoveComponent()
        {
            Velocity = new Vector2();
            Speed = 1.0f;
        }

        public MoveComponent(float speed)
        {
            Velocity = new Vector2();
            Speed = speed;
        }
    }
}
