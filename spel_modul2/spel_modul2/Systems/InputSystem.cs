using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
                switch (playerControl.ControllerType)
                {
                    case "Keyboard":

                        break;
                    case "Gamepad1":
                        break;
                    case "Gamepad2":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
