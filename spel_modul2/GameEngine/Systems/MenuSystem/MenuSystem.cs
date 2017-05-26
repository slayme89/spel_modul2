using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace GameEngine.Systems
{
    public class MenuSystem : ISystem
    {
        private bool IsActive = false;
        private bool IsInit = false;
        private int[] ActiveButtonsList = new int[10];
        private int SelectedButton;
        private float SelectCooldown = 0.0f;
        private float MaxSelectCooldown = 0.1f;

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            // see if the menu button is pressed inside the game or menu
            foreach (var contEntity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent contComp = (PlayerControlComponent)contEntity.Value;
                
                //Reset buttonList
                ActiveButtonsList = null;
                ActiveButtonsList = new int[10];
                
                //Add all active buttons in the list
                int i = 0;
                foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
                {
                    MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                    if (buttonComp.IsActive)
                    {
                        ActiveButtonsList[i] = button.Key;
                        i++;
                    }
                }

                // Check if its time to initialize the menu
                if (StateManager.GetInstance().State == GameState.Menu)
                {
                    if (!IsInit)
                        InitMenu();
                    if (!IsActive)
                        IsActive = true;
                    
                    // Apply effects on menu background
                    foreach (var menuBackground in cm.GetComponentsOfType<MenuBackgroundComponent>())
                    {
                        MenuBackgroundComponent men = (MenuBackgroundComponent)menuBackground.Value;

                        if (men.HasFadingEffect && men.IsActive)
                            FadeEffect(gameTime, men);
                        if (men.HasMovingEffect && men.IsActive)
                            MoveEffect(gameTime, men);
                    }

                    // Makes the menu button selection smooth
                    if (SelectCooldown <= 0.0f)
                    {
                        Vector2 stickDir = contComp.Movement.GetDirection();
                        //Check navigation in the menu
                        if (Math.Abs(stickDir.Y) > 0.5f)
                        {
                            //if the stick has been pushed in a direction
                            Point direction = MoveSystem.CalcDirection(stickDir.X, stickDir.Y);

                            cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Ishighlighted = false;
                            SelectedButton = (SelectedButton + direction.Y) % i;
                            if (SelectedButton < 0)
                                SelectedButton = i - 1;
                            cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Ishighlighted = true;
                            SelectCooldown = MaxSelectCooldown;
                        }
                    }
                    else
                        SelectCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    // Check if someone pressed "use" on a highlighted menu button
                    if (contComp.Interact.IsButtonDown())
                        cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[SelectedButton]).Use();
                }

                //Enter the menu
                if (contComp.Menu.IsButtonDown() == true && !IsActive)
                {
                    SelectedButton = 0;
                    StateManager.GetInstance().State = GameState.Menu;
                    IsActive = true;

                    if (IsInit == false)
                        InitMenu();
                }

                //Exit the menu if menu button is pressed
                else if (contComp.Menu.IsButtonDown() && IsActive)
                {
                    ClearMenu();
                    IsActive = false;
                    IsInit = false;
                    StateManager.GetInstance().State = GameState.Game;
                }

                //If State is changed to "Game"
                if (StateManager.GetInstance().State == GameState.Game)
                {
                    ClearMenu();
                    IsActive = false;
                    IsInit = false;
                }
            }
        }


        // Initializes the main menu
        private void InitMenu()
        {
            ComponentManager cm = ComponentManager.GetInstance();
            int i = 0;
            
            //Set all Main menu buttons to "active" and add them to the buttonList
            foreach (var button in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)button.Value;

                if (buttonComp.Type == MenuButtonType.Main)
                {
                    buttonComp.IsActive = true;
                    ActiveButtonsList[i] = button.Key;
                    i++;
                }
            }

            MenuButtonComponent highligtComp = cm.GetComponentForEntity<MenuButtonComponent>(ActiveButtonsList[0]);
            highligtComp.Ishighlighted = true;

            //Set Main menu background to "active"
            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;

                if (backgroundComp.Type == MenuBackgroundType.Main)
                    backgroundComp.IsActive = true;
            }
            IsInit = true;
        }

        public void MoveEffect(GameTime gameTime, MenuBackgroundComponent backgroundComp)
        {
            //Decrement the delay by the number of seconds that have elapsed since
            //the last time that the Update method was called
            backgroundComp.mFadeDelayMove -= gameTime.ElapsedGameTime.TotalSeconds;

            if (backgroundComp.mFadeDelayMove <= 0)
            {
                backgroundComp.mFadeDelayMove = .035;

                // Move Right
                if (backgroundComp.Position.X > -500 && backgroundComp.Position.Y == 0)
                {
                    backgroundComp.Position -= new Point(1, 0);
                }
                // Move Down
                if (backgroundComp.Position.X == -500 && backgroundComp.Position.Y <= 0)
                {
                    backgroundComp.Position -= new Point(0, 1);
                }
                // Move Left
                if (backgroundComp.Position.X <= 0 && backgroundComp.Position.Y == -500)
                {
                    backgroundComp.Position -= new Point(-1, 0);
                }
                // Move up
                if (backgroundComp.Position.X == 0 && backgroundComp.Position.Y <= 0)
                {
                    backgroundComp.Position -= new Point(0, -1);
                }
            }
        }

        public void FadeEffect(GameTime gameTime, MenuBackgroundComponent backgroundComp)
        {
            //Decrement the delay by the number of seconds that have elapsed since
            //the last time that the Update method was called
            backgroundComp.mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            //If the Fade delays has dropped below zero, then it is time to 
            //fade in/fade out the image a little bit more.
            if (backgroundComp.mFadeDelay <= 0)
            {
                //Reset the Fade delay
                backgroundComp.mFadeDelay = .1;

                //Increment/Decrement the fade value for the image
                backgroundComp.mAlphaValue += backgroundComp.mFadeIncrement;

                //If the AlphaValue is equal or above the max Alpha value or
                //has dropped below or equal to the min Alpha value, then 
                //reverse the fade
                if (backgroundComp.mAlphaValue <= 210 || backgroundComp.mAlphaValue >= 255)
                {
                    backgroundComp.mFadeIncrement *= -1;
                }
            }
        }

        private void ClearMenu()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            //Set all menu buttons to "NonActive"
            foreach (var buttonEntity in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent button = (MenuButtonComponent)buttonEntity.Value;
                button.IsActive = false;
                button.Ishighlighted = false;
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
