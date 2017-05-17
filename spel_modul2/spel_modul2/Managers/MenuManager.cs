namespace GameEngine
{
    class MenuManager
    {

        // Play
        public static void Play()
        {
            ClearAllButtons();
            ClearAllBbackgrounds();
            StateManager.GetInstance().SetState("Game");
        }

        public static void Options()
        {
            ComponentManager cm = ComponentManager.GetInstance();
            ClearAllButtons();

            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                if (buttonComp.Name.Substring(0, 7) == "Options")
                {
                    buttonComp.IsActive = true;
                }
            }
        }




        public static void ClearAllButtons()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                buttonComp.IsActive = false;
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
