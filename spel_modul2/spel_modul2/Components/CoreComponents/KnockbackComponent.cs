namespace GameEngine
{
    class KnockbackComponent : IComponent
    {
        public int Knockback { get; set; }
        public KnockbackComponent(int knockback)
        {
            Knockback = knockback;
        }
    }
}
