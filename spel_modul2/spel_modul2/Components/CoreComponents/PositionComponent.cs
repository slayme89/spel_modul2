using Microsoft.Xna.Framework;

namespace GameEngine
{
    class PositionComponent : IComponent
    {
        public Point position;

        public PositionComponent(Point position)
        {
            this.position = position;
        }

        public PositionComponent(int x, int y)
        {
            position = new Point(x, y);
        }
    }
}
