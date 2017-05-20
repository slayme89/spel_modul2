using GameEngine.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Managers;
using GameEngine.Systems;
using Game.Components;

namespace Game.Systems
{
    public class InventoryLoaderSystem : ISystem
    {
        public void Load(ContentManager content)
        {
            var inventories = ComponentManager.GetInstance().GetComponentsOfType<InventoryComponent>();

            foreach (var entity in inventories)
            {
                InventoryComponent invComp = (InventoryComponent)entity.Value;
                invComp.Font = content.Load<SpriteFont>("NewSpriteFont");
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
