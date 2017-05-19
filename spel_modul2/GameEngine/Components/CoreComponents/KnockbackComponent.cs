using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class KnockbackComponent : IComponent
    {
        public Vector2 KnockbackDir { get; set; }
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
