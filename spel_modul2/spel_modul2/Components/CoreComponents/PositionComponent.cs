using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class PositionComponent : IComponent
    {
        public Vector2 position;

        public PositionComponent(Vector2 position)
        {
            this.position = position;
        }

        public PositionComponent(int x, int y)
        {
            position = new Vector2(x, y);
        }
    }
}
