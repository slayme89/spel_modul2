using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Systems
{
    public class InputSystem : ISystem
    {
        private KeyboardState previousKeyboardState;
        private GamePadState previousGamepadState1;
        private GamePadState previousGamepadState2;

        public InputSystem()
        {
            previousKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                previousGamepadState1 = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                previousGamepadState2 = GamePad.GetState(PlayerIndex.Two);
        }

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;
                GamePadState gamepad;
                switch (playerControl.ControllerType)
                {
                    case ControllerType.Keyboard:
                        // Movement
                        KeyboardState keyboard = Keyboard.GetState();
                        Vector2 dir = new Vector2();
                        if (keyboard.IsKeyDown(Keys.W))
                        {
                            if (keyboard.IsKeyDown(Keys.D))
                                dir = new Vector2(0.7071f, -0.7071f);
                            else if(keyboard.IsKeyDown(Keys.A))
                                dir = new Vector2(-0.7071f, -0.7071f);
                            else
                                dir = new Vector2(0.0f, -1.0f);
                        }
                        else if (keyboard.IsKeyDown(Keys.S))
                        {
                            if (keyboard.IsKeyDown(Keys.D))
                                dir = new Vector2(0.7071f, 0.7071f);
                            else if (keyboard.IsKeyDown(Keys.A))
                                dir = new Vector2(-0.7071f, 0.7071f);
                            else
                                dir = new Vector2(0.0f, 1.0f);
                        }
                        else if (keyboard.IsKeyDown(Keys.A))
                            dir = new Vector2(-1.0f, 0.0f);
                        else if (keyboard.IsKeyDown(Keys.D))
                            dir = new Vector2(1.0f, 0.0f);
                        else
                            dir = new Vector2(0.0f, 0.0f);
                        playerControl.Movement.SetDirection(dir);

                        MouseState mouse = Mouse.GetState();
                        // Attack
                        if (keyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                            playerControl.Attack.SetButton(true);
                        else
                            playerControl.Attack.SetButton(false);
                        // Interact
                        if(keyboard.IsKeyDown(Keys.E) && previousKeyboardState.IsKeyUp(Keys.E))
                            playerControl.Interact.SetButton(true);
                        else
                            playerControl.Interact.SetButton(false);
                        // Menu
                        if (keyboard.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                            playerControl.Menu.SetButton(true);
                        else
                            playerControl.Menu.SetButton(false);
                        // Inventory
                        if (keyboard.IsKeyDown(Keys.C) && previousKeyboardState.IsKeyUp(Keys.C))
                            playerControl.Inventory.SetButton(true);
                        else
                            playerControl.Inventory.SetButton(false);
                        // Back
                        if (keyboard.IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
                            playerControl.Back.SetButton(true);
                        else
                            playerControl.Back.SetButton(false);
                        // Actionbar 1
                        if (keyboard.IsKeyDown(Keys.D1) && previousKeyboardState.IsKeyUp(Keys.D1))
                            playerControl.ActionBar1.SetButton(true);
                        else
                            playerControl.ActionBar1.SetButton(false);
                        // Actionbar 2
                        if (keyboard.IsKeyDown(Keys.D2) && previousKeyboardState.IsKeyUp(Keys.D2))
                            playerControl.ActionBar2.SetButton(true);
                        else
                            playerControl.ActionBar2.SetButton(false);
                        // Actionbar 3
                        if (keyboard.IsKeyDown(Keys.D3) && previousKeyboardState.IsKeyUp(Keys.D3))
                            playerControl.ActionBar3.SetButton(true);
                        else
                            playerControl.ActionBar3.SetButton(false);
                        // Actionbar 4
                        if (keyboard.IsKeyDown(Keys.D4) && previousKeyboardState.IsKeyUp(Keys.D4))
                            playerControl.ActionBar4.SetButton(true);
                        else
                            playerControl.ActionBar4.SetButton(false);
                        // Set previous keyboard state
                        previousKeyboardState = Keyboard.GetState();
                        break;

                    case ControllerType.Gamepad1:
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.One);
                        GamePadInputs(gamepad, previousGamepadState1, playerControl);
                        // Set previous gamepad state
                        previousGamepadState1 = GamePad.GetState(PlayerIndex.One);
                        break;

                    case ControllerType.Gamepad2:
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.Two);
                        GamePadInputs(gamepad, previousGamepadState2, playerControl);
                        // Set previous gamepad state
                        previousGamepadState2 = GamePad.GetState(PlayerIndex.Two);
                        break;
                    default:
                        break;
                }
            }
        }
        private void GamePadInputs(GamePadState gamepad, GamePadState previousGamepadState, PlayerControlComponent playerControl)
        {
            playerControl.Movement.SetDirection(new Vector2(gamepad.ThumbSticks.Left.X, -gamepad.ThumbSticks.Left.Y));
            // Menu
            if (gamepad.IsButtonDown(Buttons.Start) && previousGamepadState.IsButtonUp(Buttons.Start))
                playerControl.Menu.SetButton(true);
            else
                playerControl.Menu.SetButton(false);

            if (gamepad.IsButtonDown(Buttons.RightTrigger))
            {
                // Actionbar 1
                if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState.IsButtonUp(Buttons.A))
                    playerControl.ActionBar1.SetButton(true);
                else
                    playerControl.ActionBar1.SetButton(false);
                // Actionbar 2
                if (gamepad.IsButtonDown(Buttons.B) && previousGamepadState.IsButtonUp(Buttons.B))
                    playerControl.ActionBar2.SetButton(true);
                else
                    playerControl.ActionBar2.SetButton(false);
                // Actionbar 3
                if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState.IsButtonUp(Buttons.X))
                    playerControl.ActionBar3.SetButton(true);
                else
                    playerControl.ActionBar3.SetButton(false);
                // Actionbar 4
                if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState.IsButtonUp(Buttons.Y))
                    playerControl.ActionBar4.SetButton(true);
                else
                    playerControl.ActionBar4.SetButton(false);
            }
            else
            {
                // Attack
                if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState.IsButtonUp(Buttons.X))
                    playerControl.Attack.SetButton(true);
                else
                    playerControl.Attack.SetButton(false);
                // Interact
                if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState.IsButtonUp(Buttons.A))
                    playerControl.Interact.SetButton(true);                 
                else
                    playerControl.Interact.SetButton(false);
                // Inventory
                if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState.IsButtonUp(Buttons.Y))
                    playerControl.Inventory.SetButton(true);
                else
                    playerControl.Inventory.SetButton(false);
                // Back
                if (gamepad.IsButtonDown(Buttons.B) && previousGamepadState.IsButtonUp(Buttons.B))
                    playerControl.Back.SetButton(true);
                else
                    playerControl.Back.SetButton(false);
            }
        }
    }
}
