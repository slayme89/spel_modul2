using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MenuSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            //If the state is set to menu
            if(StateManager.GetInstance().GetState() == "menu")
            {


            }


            //If the state is set to game-mode
            if (StateManager.GetInstance().GetState() == "game")
            {


            }
        }
    }
}
