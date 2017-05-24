using GameEngine.Components;

namespace GameEngine.Managers
{
    public class MenuManager
    {
        // Go to Play mode
        public static void Play()
        {
            ClearAllButtons();
            ClearAllBbackgrounds();
            StateManager.GetInstance().State = GameState.Game;
        }

        //Quit game
        public static void Quit()
        {
            StateManager.GetInstance().State = GameState.Exit;
        }

        public static void ClearAllButtons()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                buttonComp.IsActive = false;
                buttonComp.Ishighlighted = false;
            }
        }

        public static void ClearAllBbackgrounds()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;

                backgroundComp.IsActive = false;
            }
        }

    }
}
