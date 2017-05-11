using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
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
