namespace GameEngine.Components
{
    public class StatsComponent : IComponent
    {
        public int Strength { get; set; } = 0;
        public int Agility { get; set; } = 0;
        public int Stamina { get; set; } = 0;
        public int Intellect { get; set; } = 0;
        public int AddStr { get; set; }
        public int AddSta { get; set; }
        public int AddAgi { get; set; }
        public int AddInt { get; set; }
        public int SpendableStats { get; set; } = 100;
        public int RemoveStats { get; set; } = 0;
        public string StatHistory = "";

        public StatsComponent(int strength, int agility, int stamina, int intellect)
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Intellect = intellect;
        }
    }
}
