namespace GameEngine.Components
{
    class HealthComponent : IComponent
    {
        /*
         * Max - maximum of healthpoints an entity can have.
         * Current - the current amount of healthpoints on an entity.
         * Alive - a check if the entity is alive or not.
        */
        public float Max { get; set; }
        public float Current { get; set; }
        public bool Alive { get; set; } = true;
    }
}
