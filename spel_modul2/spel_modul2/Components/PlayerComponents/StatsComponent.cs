namespace GameEngine
{
    class StatsComponent : IComponent
    {
        public int Strength { get; set; }
        public int Agillity { get; set; }
        public int Stamina { get; set; }
        public int Intellect { get; set; }
        public int AddStats { get; set; }
        public int RemoveStats { get; set; }
        public string StatHistory;

        public StatsComponent(int strength, int agillity, int stamina, int intellect)
        {
            Strength = strength;
            Agillity = agillity;
            Stamina = stamina;
            Intellect = intellect;
            AddStats = 0;
            StatHistory = "";
            RemoveStats = 0;
        }
    }
}
