namespace GameEngine.Managers
{
    enum GameState
    {
        Main,
        Menu,
        Game,
        Exit,
    }
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
