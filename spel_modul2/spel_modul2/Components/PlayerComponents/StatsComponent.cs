namespace GameEngine
{
    class StatsComponent : IComponent
    {
        public int Strength { get; set; } = 0;
        public int Agillity { get; set; } = 0;
        public int Stamina { get; set; } = 0;
        public int Intellect { get; set; } = 0;
        public int AddStr { get; set; }
        public int AddSta { get; set; }
        public int AddAgi { get; set; }
        public int AddInt { get; set; }
        public int SpendableStats { get; set; } = 100;
        public int RemoveStats { get; set; } = 0;
        public string StatHistory = "";

        public StatsComponent(int strength, int agillity, int stamina, int intellect)
        {
            Strength = strength;
            Agillity = agillity;
            Stamina= stamina;
            Intellect = intellect;
        }
    }
}
