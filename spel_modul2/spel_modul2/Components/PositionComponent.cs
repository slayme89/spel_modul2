using Microsoft.Xna.Framework;

namespace GameEngine
{
    class PositionComponent : IComponent
    {
        public Vector2 position;

        public PositionComponent(Vector2 position)
        {
            this.position = position;
        }

        public PositionComponent(float x, float y)
        {
            position = new Vector2(x, y);
        }
    }
}
