using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    public class ItemIconLoaderSystem : ISystem
    {
        public void Load(ContentManager content)
        {
            var items = ComponentManager.GetInstance().GetComponentsOfType<ItemComponent>();

            foreach (ItemComponent itemComp in items.Values)
            {
                itemComp.ItemIcon = content.Load<Texture2D>("ItemIcons/" + itemComp.TextureFileName);
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
