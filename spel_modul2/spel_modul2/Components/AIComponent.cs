using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AIComponent : IComponent
    {
        public AIComponent(int x, int y)
        {
            Destination = new Point(x, y);

        }
        public Point Destination { get; set; }
    }
}
