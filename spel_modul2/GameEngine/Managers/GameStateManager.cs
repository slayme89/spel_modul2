namespace GameEngine.Managers
{
    public enum GameState { Menu, Game, OnePlayerGame, TwoPlayerGame, Restart, Exit };

    public class GameStateManager
    {
        static GameStateManager instance;
        public GameState State { get; set; }

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
