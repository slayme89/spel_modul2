using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    class MenuSystem : ISystem
    {
        private bool IsActive = false;
        private bool IsInit = false;
        //private Dict<MenuButtonComponent> buttonList = new Dictionary<MenuButtonComponent>();

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Vector2 stickDir;

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


                stickDir = contComp.Movement.GetDirection();
                //Check navigation in the menu
                if (Math.Abs(stickDir.X) > 0.5f || Math.Abs(stickDir.Y) > 0.5f)
                {
                    //if the stick has been pushed in a direction
                    Point direction = MoveSystem.CalcDirection(stickDir.X, stickDir.Y);

                    
                }


            }
        }

        private void InitMenu()
        {
            ComponentManager cm = ComponentManager.GetInstance();
           
            //Set all Main menu buttons to "active" and add all buttons to the buttonList
            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;
                //buttonList.Add(buttonComp);

                if (buttonComp.Name.Substring(0, 4) == "Main")
                    buttonComp.IsActive = true;
                if (buttonComp.Name.Substring(4, 8) == "Play")
                    buttonComp.Ishighlighted = true;
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
