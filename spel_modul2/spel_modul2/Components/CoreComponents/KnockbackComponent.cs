namespace GameEngine
{
    class KnockbackComponent : IComponent
    {
        public int Weight { get; set; }
        public KnockbackComponent(int weight)
        {
            Weight = weight;
        }
    }
}
