namespace GameEngine.Components
{
    public enum InteractType { Trap, Talk, Loot };

    public class InteractComponent : IComponent
    {
        public InteractType Type { get; set; }

        public InteractComponent(InteractType type)
        {
            Type = type;
        }
    }
}
