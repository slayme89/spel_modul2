using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class InventoryLoaderSystem : ISystem
    {
        public void Load(ContentManager content)
        {
            var inventories = ComponentManager.GetInstance().GetComponentsOfType<InventoryComponent>();

            foreach (InventoryComponent invComp in inventories.Values)
            {
                invComp.font = content.Load<SpriteFont>("NewSpriteFont");
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
