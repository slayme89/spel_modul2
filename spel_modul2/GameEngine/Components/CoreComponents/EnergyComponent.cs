using System;

namespace GameEngine.Components
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
