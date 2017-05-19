namespace GameEngine.Components
{
    public class HealthComponent : IComponent
    {
        public bool IsAlive { get; set; } = true;
        public int Max { get; set; }
        public int Current { get; set; }
        public float DeathTimer { get; set; }

        public HealthComponent(int maxHealth)
        {
            Max = maxHealth;
            Current = Max;
        }

        public HealthComponent(int maxHealth, float deathTimer)
        {
            Max = maxHealth;
            Current = Max;
            DeathTimer = deathTimer;
        }
    }
}
