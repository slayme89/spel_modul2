namespace GameEngine
{
    class LevelComponent : IComponent
    {
        public int StartLevel { get; set; } = 1;
        public int CurrentLevel { get; set; }
        public float Experience { get; set; } = 0;


        public LevelComponent()
        {
        }

        public LevelComponent(int startLevel)
        {
            StartLevel = startLevel;
            CurrentLevel = startLevel;
        }
    }
}
