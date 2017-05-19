using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class MoveComponent : IComponent
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public Point Direction { get; set; }
        public bool CanMove { get; set; }

        public MoveComponent()
        {
            CanMove = true;
            Velocity = new Vector2();
            Speed = 1.0f;
            Direction = new Point(0, 1);
        }

        public MoveComponent(float speed)
        {
            CanMove = true;
            Velocity = new Vector2();
            Speed = speed;
            Direction = new Point(0, 1);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
