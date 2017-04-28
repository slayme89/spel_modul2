using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class InputSystem : ISystem
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
                    case "Keyboard":
                        // Movement
                        MouseState mouse = Mouse.GetState();
                        if(mouse.LeftButton == ButtonState.Pressed)
                        {
                            PositionComponent position = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                            if (position == null)
                                throw new Exception("Couldn't find a position component when handling input. Entity ID: " + entity.Key);
                            Point direction = new Point(position.position.X - mouse.X, position.position.Y - mouse.Y);
                            float distance = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                            Vector2 dir = new Vector2(direction.X / distance, direction.Y / distance);
                            playerControl.Movement.SetDirection(dir);
                        }
                        KeyboardState keyboard = Keyboard.GetState();
                        // Attack
                        if(mouse.RightButton == ButtonState.Pressed)
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
                    case "Gamepad1":
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.One);
                        playerControl.Movement.SetDirection(gamepad.ThumbSticks.Left);
                        // Menu
                        if (gamepad.IsButtonDown(Buttons.Start) && previousGamepadState1.IsButtonUp(Buttons.Start))
                            playerControl.Menu.SetButton(true);
                        else
                            playerControl.Menu.SetButton(false);

                        if(gamepad.IsButtonDown(Buttons.RightTrigger))
                        {
                            // Actionbar 1
                            if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState1.IsButtonUp(Buttons.A))
                                playerControl.ActionBar1.SetButton(true);
                            else
                                playerControl.ActionBar1.SetButton(false);
                            // Actionbar 2
                            if (gamepad.IsButtonDown(Buttons.B) && previousGamepadState1.IsButtonUp(Buttons.B))
                                playerControl.ActionBar2.SetButton(true);
                            else
                                playerControl.ActionBar2.SetButton(false);
                            // Actionbar 3
                            if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState1.IsButtonUp(Buttons.X))
                                playerControl.ActionBar3.SetButton(true);
                            else
                                playerControl.ActionBar3.SetButton(false);
                            // Actionbar 4
                            if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState1.IsButtonUp(Buttons.Y))
                                playerControl.ActionBar4.SetButton(true);
                            else
                                playerControl.ActionBar4.SetButton(false);
                        }
                        else
                        {
                            // Attack
                            if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState1.IsButtonUp(Buttons.X))
                                playerControl.Attack.SetButton(true);
                            else
                                playerControl.Attack.SetButton(false);
                            // Interact
                            if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState1.IsButtonUp(Buttons.A))
                                playerControl.Interact.SetButton(true);
                            else
                                playerControl.Interact.SetButton(false);
                            // Inventory
                            if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState1.IsButtonUp(Buttons.Y))
                                playerControl.Inventory.SetButton(true);
                            else
                                playerControl.Inventory.SetButton(false);
                        }
                        // Set previous gamepad state
                        previousGamepadState1 = GamePad.GetState(PlayerIndex.One);
                        break;
                    case "Gamepad2":
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.Two);
                        playerControl.Movement.SetDirection(gamepad.ThumbSticks.Left);
                        // Menu
                        if (gamepad.IsButtonDown(Buttons.Start) && previousGamepadState2.IsButtonUp(Buttons.Start))
                            playerControl.Menu.SetButton(true);
                        else
                            playerControl.Menu.SetButton(false);

                        if (gamepad.IsButtonDown(Buttons.RightTrigger))
                        {
                            // Actionbar 1
                            if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState2.IsButtonUp(Buttons.A))
                                playerControl.ActionBar1.SetButton(true);
                            else
                                playerControl.ActionBar1.SetButton(false);
                            // Actionbar 2
                            if (gamepad.IsButtonDown(Buttons.B) && previousGamepadState2.IsButtonUp(Buttons.B))
                                playerControl.ActionBar2.SetButton(true);
                            else
                                playerControl.ActionBar2.SetButton(false);
                            // Actionbar 3
                            if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState2.IsButtonUp(Buttons.X))
                                playerControl.ActionBar3.SetButton(true);
                            else
                                playerControl.ActionBar3.SetButton(false);
                            // Actionbar 4
                            if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState2.IsButtonUp(Buttons.Y))
                                playerControl.ActionBar4.SetButton(true);
                            else
                                playerControl.ActionBar4.SetButton(false);
                        }
                        else
                        {
                            // Attack
                            if (gamepad.IsButtonDown(Buttons.X) && previousGamepadState2.IsButtonUp(Buttons.X))
                                playerControl.Attack.SetButton(true);
                            else
                                playerControl.Attack.SetButton(false);
                            // Interact
                            if (gamepad.IsButtonDown(Buttons.A) && previousGamepadState2.IsButtonUp(Buttons.A))
                                playerControl.Interact.SetButton(true);
                            else
                                playerControl.Interact.SetButton(false);
                            // Inventory
                            if (gamepad.IsButtonDown(Buttons.Y) && previousGamepadState2.IsButtonUp(Buttons.Y))
                                playerControl.Inventory.SetButton(true);
                            else
                                playerControl.Inventory.SetButton(false);
                        }
                        // Set previous gamepad state
                        previousGamepadState2 = GamePad.GetState(PlayerIndex.Two);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
