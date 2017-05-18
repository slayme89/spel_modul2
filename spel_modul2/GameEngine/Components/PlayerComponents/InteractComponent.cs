namespace GameEngine.Components
{
    class InteractComponent : IComponent
    {
        public InteractType Type { get; set; }

        public InteractComponent(InteractType type)
        {
            Type = type;
        }
    }

    public enum InteractType { Trap, Loot };
}
