namespace GameEngine.Managers
{
    public enum GameState { Menu, Game, Exit };

    class StateManager
    {
        static StateManager instance;
        public GameState State { get; set; }
     

        static StateManager()
        {
            instance = new StateManager();
        }

        public static StateManager GetInstance()
        {
            return instance;
        }
    }
}
