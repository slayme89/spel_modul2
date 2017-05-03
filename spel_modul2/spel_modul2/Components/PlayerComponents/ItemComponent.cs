using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class ItemComponent : IComponent
    {
        public int InventoryPosition { get; set; }
        public string TextureFileName { get; set; }
        public Texture2D ItemIcon { get; set; }

        public ItemComponent(string ItemIconFileName)
        {
            TextureFileName = ItemIconFileName;
        }
    }
}
