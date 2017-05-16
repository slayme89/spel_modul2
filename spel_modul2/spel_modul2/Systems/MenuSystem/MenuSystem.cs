using System;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MenuSystem : ISystem, IRenderSystem
    {
        private bool IsActive = false;

        public void Render(RenderHelper renderHelper)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            
            // see if the menu button is pressed inside the game or menu
            foreach(var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent cont = (PlayerControlComponent)entity.Value;

                //Enter the menu
                if (cont.Menu.IsButtonDown() && IsActive == false)
                {
                    IsActive = true;
                    StateManager.GetInstance().SetState("menu");
                }
                    
                //Exit the menu
                else if(cont.Menu.IsButtonDown() && IsActive == true)
                {
                    IsActive = false;
                    StateManager.GetInstance().SetState("game");
                }

                foreach(var buttonEntity in cm.GetComponentsOfType<MenuButtonComponent>())
                {
                    MenuButtonComponent menubutton = (MenuButtonComponent)buttonEntity.Value;

                    //Check if button is active
                    if (menubutton.IsActive)
                    {
                        //Check if button has TextureComponent, if so render it
                        if (cm.HasEntityComponent<TextureComponent>(buttonEntity.Key))
                        {

                        }
                    }


                }

        



                    
            }
        }
    }
}
