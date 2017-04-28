using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AIComponent : IComponent
    {
        public Point Destination { get; set; }

        public AIComponent(int x, int y)
        {
            Destination = new Point(x, y);

        }
    }
}
