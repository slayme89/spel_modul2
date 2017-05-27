using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;
using GameEngine.Managers;

namespace Game.Components
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
        public string TextureFileName { get; set; }
        public Texture2D ItemIcon { get; set; }
        public ItemType Type { get; set; }
        public Action Use { get; set; }
        public bool IsItem { get; set; } = true;

        public ItemComponent(Action action, string itemIconFileName, ItemType type)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Use = action;
            Type = type;
            TextureFileName = itemIconFileName;
            ItemIcon = rm.GetResource<Texture2D>("ItemIcons/" + TextureFileName);
        }

        public object Clone()
        {
            ResourceManager rm = ResourceManager.GetInstance();
            ItemComponent o = (ItemComponent)MemberwiseClone();
            ItemIcon = rm.GetResource<Texture2D>("ItemIcons/" + TextureFileName);
            return o;
        }
    }
}
