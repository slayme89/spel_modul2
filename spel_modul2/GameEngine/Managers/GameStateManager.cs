namespace GameEngine.Managers
{
    public enum GameState { Menu, Game, OnePlayerGame, TwoPlayerGame, GameOver, Restart, Exit };

    public class GameStateManager
    {
        static GameStateManager instance;
        public GameState m_State;
        public GameState State
        {
            get { return m_State; }
            set
            {
                LastState = State;
                m_State = value;
            }
        }

        public GameState LastState { get; set; }

        static GameStateManager()
        {
            instance = new GameStateManager();
        }

        public static GameStateManager GetInstance()
        {
            return instance;
        }
    }
}
