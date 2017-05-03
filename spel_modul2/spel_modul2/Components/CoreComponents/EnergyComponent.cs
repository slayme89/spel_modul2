﻿namespace GameEngine
{
    class EnergyComponent : IComponent
    {
        public int Max { get; set; }
        public int Current { get; set; }

        public EnergyComponent(int max, int current)
        {
            Max = max;
            Current = Max;
        }
    }
}