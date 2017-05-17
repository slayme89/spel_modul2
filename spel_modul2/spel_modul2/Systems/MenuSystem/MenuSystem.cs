using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MenuSystem : ISystem
    {
        private bool IsActive = false;
        private bool IsInit = false;

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            // see if the menu button is pressed inside the game or menu
            foreach (var contEntity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent contComp = (PlayerControlComponent)contEntity.Value;

                //Enter the menu
                if (contComp.Menu.IsButtonDown() == true && IsActive == false)
                {
                    StateManager.GetInstance().SetState("Menu");
                    IsActive = true;
                    
                    if (IsInit == false)
                        InitMenu();
                }

                //Exit the menu if menu button is pressed
                else if (contComp.Menu.IsButtonDown() && IsActive == true)
                {
                    ClearMenu();
                    IsActive = false;
                    IsInit = false;
                    StateManager.GetInstance().SetState("Game");
                }
                
                //If State is changed to "Game"
                if (StateManager.GetInstance().GetState() == "Game")
                {
                    ClearMenu();
                    IsActive = false;
                    IsInit = false;
                }
            }
        }

        private void InitMenu()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            //Set all Main menu button to "active"
            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                if (buttonComp.Name.Substring(0, 4) == "Main")
                    buttonComp.IsActive = true;
            }

            //Set Mainmenu background to "active"
            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;

                if (backgroundComp.Name.Substring(0, 4) == "Main")
                    backgroundComp.IsActive = true;
            }
            IsInit = true;
        }

        private void ClearMenu()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            //Set all menu buttons to "NonActive"
            foreach (var buttonEntity in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent button = (MenuButtonComponent)buttonEntity.Value;
                button.IsActive = false;
            }

            //Set all menu backgrounds to "NonActive"
            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;
                backgroundComp.IsActive = false;
            }
        }
    }
}
