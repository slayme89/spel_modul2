using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class AIComponent : IComponent
    {
        public Point Destination { get; set; }
        public bool IsFriendly { get; set; }
        public float DetectRange { get; set; }
        public int TargetEntity { get; set; }

        public AIComponent(int x, int y, bool isFriendly)
        {
            Destination = new Point(x, y);
            IsFriendly = isFriendly;
            DetectRange = 200;
            TargetEntity = 0;
        }
    }
}
