using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Systems;
using Game.Components;
using Game.Managers;


namespace Game.Systems
{
    public class MenuSystem : ISystem
    {
        private List<int> ActiveButtonsList;
        private int SelectedButton;
        private float SelectCooldown = 0.0f;
        private float MaxSelectCooldown = 0.2f;
        EntityFactory factory = new EntityFactory();

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            ActivateMenuButtons();
            ActivateMenuBackground();
            ClearMenu();
            
            foreach (var controlEntity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent controlComp = (PlayerControlComponent)controlEntity.Value;
                
                // If we are in some kind of menu state
                if (GameStateManager.GetInstance().State == GameState.Menu)
                {
                    
                    // Apply effects on menu background
                    foreach (var menuBackground in cm.GetComponentsOfType<MenuBackgroundComponent>())
                    {
                        MenuBackgroundComponent men = (MenuBackgroundComponent)menuBackground.Value;

                        if (men.HasFadingEffect && men.IsActive)
                            FadeEffect(gameTime, men);
                        if (men.HasMovingEffect && men.IsActive)
                            MoveEffect(gameTime, men);
                    }

                    SelectCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    // Makes the menu button selection smooth
                    if (SelectCooldown <= 0.0f)
                    {
                        Vector2 stickDir = controlComp.Movement.GetDirection();
                        //Check navigation in the menu
                        if (Math.Abs(stickDir.Y) > 0.5f)
                        {
                            //if the stick has been pushed in a direction
                            Point direction = MoveSystem.CalcDirection(stickDir.X, stickDir.Y);

                            cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Ishighlighted = false;
                            SelectedButton = (SelectedButton + direction.Y) % ActiveButtonsList.Count;
                            if (SelectedButton < 0)
                                SelectedButton = ActiveButtonsList.Count - 1;
                            cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Ishighlighted = true;
                            SelectCooldown = MaxSelectCooldown;
                        }
                    }

                    // Check if highlighted button was pressed "use"
                    if (controlComp.Interact.IsButtonDown())
                        cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Use();

                    if (GameStateManager.GetInstance().State == GameState.TwoPlayerGame)
                    {
                        cm.AddEntityWithComponents(factory.CreatePlayerTwo(256, 128));
                        MenuStateManager.GetInstance().State = MenuState.None;
                        GameStateManager.GetInstance().State = GameState.Game;
                        break;
                    }

                    // Exit the Pausemenu if menu button is pressed from GameState "Menu"
                    if (controlComp.Menu.IsButtonDown() &&
                        GameStateManager.GetInstance().State == GameState.Menu &&
                        MenuStateManager.GetInstance().State == MenuState.PauseMainMenu)
                    {
                        GameStateManager.GetInstance().State = GameState.Game;
                        MenuStateManager.GetInstance().State = MenuState.ExitPauseMenu;
                    }
                }

                // Enter the PauseMenu if menu button is pressed from GameState "Game"
                if (controlComp.Menu.IsButtonDown() &&
                    GameStateManager.GetInstance().State == GameState.Game && 
                    MenuStateManager.GetInstance().State == MenuState.None)
                {
                    GameStateManager.GetInstance().State = GameState.Menu;
                    MenuStateManager.GetInstance().State = MenuState.PauseMainMenu;
                }

                if(MenuStateManager.GetInstance().State == MenuState.ExitPauseMenu)
                    MenuStateManager.GetInstance().State = MenuState.None;


            }
        }

        // Initializes Menu buttons
        private void ActivateMenuButtons()
        {
            if (GameStateManager.GetInstance().State != GameState.Game)
            {
                ComponentManager cm = ComponentManager.GetInstance();
                ActiveButtonsList = new List<int>();

                //Set buttons to "active" and add them to the buttonList
                foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
                {
                    MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;
                   
                    switch (MenuStateManager.GetInstance().State)
                    {
                        case MenuState.MainMenu:
                            if (buttonComp.Type == MenuButtonType.MainMenuButton)
                                buttonComp.IsActive = true;
                            else
                                buttonComp.IsActive = false;
                            break;
                        case MenuState.PauseMainMenu:
                            if (buttonComp.Type == MenuButtonType.PauseMainMenuButton)
                                buttonComp.IsActive = true;
                            else
                                buttonComp.IsActive = false;
                            break;
                        case MenuState.ExitPauseMenu:
                            buttonComp.IsActive = false;
                            break;
                        case MenuState.None:
                            buttonComp.IsActive = false;
                            break;
                    }
                    
                    if (buttonComp.IsActive)
                    {
                        ActiveButtonsList.Add(button.Key);
                    }
                }
                int checkButtons = 0;
                foreach (var button in ActiveButtonsList)
                {
                    if (cm.GetComponentForEntity<MenuButtonComponent>(button).Ishighlighted)
                        checkButtons++;
                }
                if (ActiveButtonsList.Count > 0 && checkButtons <= 0)
                {
                    SelectedButton = 0;
                    cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[0]).Ishighlighted = true;
                }
            }
        }
        
        private void ActivateMenuBackground()
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (GameStateManager.GetInstance().State == GameState.Menu)
            {
                foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
                {
                    MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;
                    backgroundComp.IsActive = true;
                }
            }
            else
            {
                foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
                {
                    MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;
                    backgroundComp.IsActive = false;
                }
            }
        }

        private void MoveEffect(GameTime gameTime, MenuBackgroundComponent backgroundComp)
        {
            backgroundComp.mFadeDelayMove -= gameTime.ElapsedGameTime.TotalSeconds;

            if (backgroundComp.mFadeDelayMove <= 0)
            {
                backgroundComp.mFadeDelayMove = .035;

                // Move Right
                if (backgroundComp.Position.X > -500 && backgroundComp.Position.Y == 0)
                    backgroundComp.Position -= new Point(1, 0);
                // Move Down
                if (backgroundComp.Position.X == -500 && backgroundComp.Position.Y <= 0)
                    backgroundComp.Position -= new Point(0, 1);
                // Move Left
                if (backgroundComp.Position.X <= 0 && backgroundComp.Position.Y == -500)
                    backgroundComp.Position -= new Point(-1, 0);
                // Move up
                if (backgroundComp.Position.X == 0 && backgroundComp.Position.Y <= 0)
                    backgroundComp.Position -= new Point(0, -1);
            }
        }

        private void FadeEffect(GameTime gameTime, MenuBackgroundComponent backgroundComp)
        {
            backgroundComp.mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (backgroundComp.mFadeDelay <= 0)
            {
                backgroundComp.mFadeDelay = .1;

                backgroundComp.mAlphaValue += backgroundComp.mFadeIncrement;

                if (backgroundComp.mAlphaValue <= 210 || backgroundComp.mAlphaValue >= 255)
                {
                    backgroundComp.mFadeIncrement *= -1;
                }
            }
        }

        private void ClearMenu()
        {
            if (GameStateManager.GetInstance().State == GameState.Game)
            {
                ComponentManager cm = ComponentManager.GetInstance();

                // Buttons
                foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
                {
                    MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;
                    buttonComp.IsActive = false;
                    buttonComp.Ishighlighted = false;
                }

                //Backgrounds
                foreach (var background in cm.GetComponentsOfType<MenuButtonComponent>())
                {
                    MenuButtonComponent backgroundComp = (MenuButtonComponent)background.Value;
                    backgroundComp.IsActive = false;
                }
            }
        }
    }
}
