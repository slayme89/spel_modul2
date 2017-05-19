using GameEngine.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    public class InventoryLoaderSystem : ISystem
    {
        public void Load(ContentManager content)
        {
            var inventories = ComponentManager.GetInstance().GetComponentsOfType<InventoryComponent>();

            foreach (InventoryComponent invComp in inventories.Values)
            {
                invComp.Font = content.Load<SpriteFont>("NewSpriteFont");
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
