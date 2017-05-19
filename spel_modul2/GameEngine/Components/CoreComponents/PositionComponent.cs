using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class PositionComponent : IComponent
    {
        public Vector2 Position;

        public PositionComponent() { }

        public PositionComponent(Vector2 position)
        {
            Position = position;
        }

        public PositionComponent(int x, int y)
        {
            Position = new Vector2(x, y);
        }
    }
}
