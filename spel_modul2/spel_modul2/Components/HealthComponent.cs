namespace GameEngine
{
    class HealthComponent : IComponent
    {
        public HealthComponent(int maxHealth)
        {
            Max = maxHealth;
            Current = Max;
        }
        public bool IsAlive { get; set; } = true;
        public int Max { get; set; }
        public int Current { get; set; }
    }
}
