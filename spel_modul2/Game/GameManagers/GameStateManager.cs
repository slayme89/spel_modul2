namespace Game.Managers
{
    public enum GameState {Splashscreen, Menu, Game, OnePlayerGame, TwoPlayerGame, GameOver, Restart, Exit };
    public class GameStateManager
    {
        static GameStateManager instance;
        public GameState m_State;
        public GameState LastState { get; set; }
        public GameState State
        {
            get { return m_State; }
            set
            {
                LastState = State;
                m_State = value;
            }
        }

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
