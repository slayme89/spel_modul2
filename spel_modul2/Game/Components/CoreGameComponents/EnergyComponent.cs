using GameEngine.Components;
using System;

namespace Game.Components
{
    public class EnergyComponent : IComponent
    {
        public float Max { get; set; }
        public float Current { get; set; }

        public EnergyComponent(int max)
        {
            Max = max;
            Current = Max;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
