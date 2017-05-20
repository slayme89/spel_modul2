using GameEngine.Components;
using System;

namespace Game.Components
{
    public enum InteractType { Trap, Talk, Loot };

    public class InteractComponent : IComponent
    {
        public InteractType Type { get; set; }

        public InteractComponent(InteractType type)
        {
            Type = type;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
