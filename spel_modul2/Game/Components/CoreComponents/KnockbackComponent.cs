using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace Game.Components
{
    public class KnockbackComponent : IComponent
    {
        public Vector2 KnockbackDir { get; set; }
        public Vector2 prevDir { get; set; }
        public float Cooldown { get; set; }
        public bool KnockbackActive { get; set; }

        public KnockbackComponent()
        {
            KnockbackActive = false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
