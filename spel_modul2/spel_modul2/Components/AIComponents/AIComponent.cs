using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AIComponent : IComponent
    {
        public Point Destination { get; set; }
        public bool IsFriendly { get; set; }
        public Point DetectRange { get; set; }
        public int TargetEntity { get; set; }

        public AIComponent(int x, int y, bool isFriendly)
        {
            Destination = new Point(x, y);
            IsFriendly = isFriendly;
            DetectRange = new Point(400, 400);
            TargetEntity = 0;
        }
    }
}
