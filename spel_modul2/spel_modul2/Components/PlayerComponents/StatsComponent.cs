namespace GameEngine
{
    class StatsComponent : IComponent
    {
        public int Strength { get; set; }
        public int Agillity { get; set; }
        public int Stamina { get; set; }
        public int Intellect { get; set; }
        public int StatPoints { get; set; }
        public int CurrNumStatPoints { get; set; }
        public string StatHistory;

        public StatsComponent(int strength, int agillity, int stamina, int intellect)
        {
            Strength = strength;
            Agillity = agillity;
            Stamina = stamina;
            Intellect = intellect;
            StatPoints = 0;
            StatHistory = "";
            CurrNumStatPoints = 0;
        }
    }
}
