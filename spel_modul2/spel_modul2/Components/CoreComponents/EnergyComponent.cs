namespace GameEngine
{
    public class EnergyComponent : IComponent
    {
        public int Max { get; set; }
        public int Current { get; set; }

        public EnergyComponent(int max)
        {
            Max = max;
            Current = Max;
        }
    }
}
