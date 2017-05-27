using GameEngine.Components;
using System.Collections.Generic;

namespace Game.Components
{
    public class HealthComponent : IComponent
    {
        public bool IsAlive { get; set; } = true;
        public int Max { get; set; }
        public int Current { get; set; }
        public float DeathTimer { get; set; }
        public List<int> IncomingDamage { get; set; }
        public int LastAttacker { get; set; }
        public int[] DamageReduction { get; set; }

        public HealthComponent(int maxHealth)
        {
            Max = maxHealth;
            Current = Max;
            DeathTimer = 20.0f;
            DamageReduction = new int[2];
            IncomingDamage = new List<int>();
        }

        public HealthComponent(int maxHealth, float deathTimer)
        {
            Max = maxHealth;
            Current = Max;
            DeathTimer = deathTimer;
            DamageReduction = new int[2];
            IncomingDamage = new List<int>();
        }

        public object Clone()
        {
            HealthComponent o = (HealthComponent)MemberwiseClone();
            o.IncomingDamage = new List<int>();
            return o;
        }
    }
}
