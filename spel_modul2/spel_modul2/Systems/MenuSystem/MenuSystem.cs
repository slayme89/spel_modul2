using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MenuSystem : ISystem
    {
        private bool IsActive = false;
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
                    
                //Enter the game
                else if(cont.Menu.IsButtonDown() && IsActive == true)
                {
                    IsActive = false;
                    StateManager.GetInstance().SetState("game");
                }


        



                    
            }
        }
    }
}
