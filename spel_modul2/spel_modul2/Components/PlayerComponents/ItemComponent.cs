using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public enum ItemType
    {
        Weapon = 0,
        Body,
        Head,
        Consumable,
        Skill
    }
    class ItemComponent : IComponent
    {
        public int InventoryPosition { get; set; }
        public int actionBarPosition { get; set; }
        public string TextureFileName { get; set; }
        public Texture2D ItemIcon { get; set; }
        public ItemType Type { get; set; }
        public ItemComponent(string ItemIconFileName, ItemType type)
        {
            Type = type;
            TextureFileName = ItemIconFileName;
        }
    }
}
