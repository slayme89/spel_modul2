using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class ItemIconLoaderSystem : ISystem
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
