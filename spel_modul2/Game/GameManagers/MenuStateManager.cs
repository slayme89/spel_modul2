using GameEngine.Managers;

namespace Game.Managers
{
    public enum MenuState { MainMenu, PauseMainMenu, MainOptionsMenu, ExitPauseMenu, None};
    public class MenuStateManager
    {
        static MenuStateManager instance;
        public MenuState State { get; set; }


        static MenuStateManager()
        {
            instance = new MenuStateManager();
        }

        public static MenuStateManager GetInstance()
        {
            return instance;
        }
        
        // MAIN MENU //

        // PLAY 1 player
        public static void MainPlayOnePlayer()
        {
            GameStateManager.GetInstance().State = GameState.Game;
        }
        
        // PLAY 2 players
        public static void MainPlayTwoPlayer()
        {
            GameStateManager.GetInstance().State = GameState.TwoPlayerGame;
        }

        // QUIT
        public static void MainQuit()
        {
            GameStateManager.GetInstance().State = GameState.Exit;
        }
 
        // PAUSE MENU //

        // RESUME
        public static void PauseResume()
        {
            GetInstance().State = MenuState.ExitPauseMenu;
            GameStateManager.GetInstance().State = GameState.OnePlayerGame;
        }

        // PAUSE QUIT - TODO
        public static void PauseQuit()
        {
            GetInstance().State = MenuState.MainMenu;
        }

    }
}
