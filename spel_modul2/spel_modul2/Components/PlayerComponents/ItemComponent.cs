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

    public class ItemComponent : IComponent, ActionBarSlotComponent
    {
        public int InventoryPosition { get; set; }
        public int actionBarPosition { get; set; }
        public string TextureFileName { get; set; }
        public Texture2D ItemIcon { get; set; }
        public ItemType Type { get; set; }
        public Action Use { get; set; }
        public bool IsItem { get; set; } = true;
        public ItemComponent(Action action, string ItemIconFileName, ItemType type)
        {
            Use = action;
            Type = type;
            TextureFileName = ItemIconFileName;
        }
    }
}
