namespace GameEngine.Components
{
    public class EnergyComponent : IComponent
    {
        public float Max { get; set; }
        public float Current { get; set; }

        public EnergyComponent(float max)
        {
            Max = max;
            Current = Max;
        }
    }
}
