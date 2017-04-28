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
                        if(mouse.LeftButton == ButtonState.Pressed)
                            playerControl.Attack.SetButton(true);
                        if (mouse.LeftButton == ButtonState.Released)
                            playerControl.Attack.SetButton(false);
                        // Interact
                        playerControl.Interact.SetButton(keyboard.IsKeyDown(Keys.E));
                        // Menu
                        playerControl.Menu.SetButton(keyboard.IsKeyDown(Keys.Escape));
                        // Inventory
                        playerControl.Inventory.SetButton(keyboard.IsKeyDown(Keys.C));
                        // Actionbar 1
                        playerControl.ActionBar1.SetButton(keyboard.IsKeyDown(Keys.D1));
                        // Actionbar 2
                        playerControl.ActionBar2.SetButton(keyboard.IsKeyDown(Keys.D2));
                        // Actionbar 3
                        playerControl.ActionBar3.SetButton(keyboard.IsKeyDown(Keys.D3));
                        // Actionbar 4
                        playerControl.ActionBar4.SetButton(keyboard.IsKeyDown(Keys.D4));
                        break;
                    case "Gamepad1":
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.One);
                        playerControl.Movement.SetDirection(gamepad.ThumbSticks.Left);
                        // Menu
                        playerControl.Menu.SetButton(gamepad.IsButtonDown(Buttons.Start));

                        if(gamepad.IsButtonDown(Buttons.RightTrigger))
                        {
                            // Actionbar 1
                            playerControl.ActionBar1.SetButton(gamepad.IsButtonDown(Buttons.A));
                            // Actionbar 2
                            playerControl.ActionBar2.SetButton(gamepad.IsButtonDown(Buttons.B));
                            // Actionbar 3
                            playerControl.ActionBar3.SetButton(gamepad.IsButtonDown(Buttons.X));
                            // Actionbar 4
                            playerControl.ActionBar4.SetButton(gamepad.IsButtonDown(Buttons.Y));
                        }
                        else
                        {
                            // Attack
                            playerControl.Attack.SetButton(gamepad.IsButtonDown(Buttons.X));
                            // Interact
                            playerControl.Interact.SetButton(gamepad.IsButtonDown(Buttons.A));
                            // Inventory
                            playerControl.Inventory.SetButton(gamepad.IsButtonDown(Buttons.Y));
                        }
                        break;
                    case "Gamepad2":
                        // Movement
                        gamepad = GamePad.GetState(PlayerIndex.Two);
                        playerControl.Movement.SetDirection(gamepad.ThumbSticks.Left);
                        // Menu
                        playerControl.Menu.SetButton(gamepad.IsButtonDown(Buttons.Start));

                        if (gamepad.IsButtonDown(Buttons.RightTrigger))
                        {
                            // Actionbar 1
                            playerControl.ActionBar1.SetButton(gamepad.IsButtonDown(Buttons.A));
                            // Actionbar 2
                            playerControl.ActionBar2.SetButton(gamepad.IsButtonDown(Buttons.B));
                            // Actionbar 3
                            playerControl.ActionBar3.SetButton(gamepad.IsButtonDown(Buttons.X));
                            // Actionbar 4
                            playerControl.ActionBar4.SetButton(gamepad.IsButtonDown(Buttons.Y));
                        }
                        else
                        {
                            // Attack
                            playerControl.Attack.SetButton(gamepad.IsButtonDown(Buttons.X));
                            // Interact
                            playerControl.Interact.SetButton(gamepad.IsButtonDown(Buttons.A));
                            // Inventory
                            playerControl.Inventory.SetButton(gamepad.IsButtonDown(Buttons.Y));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
