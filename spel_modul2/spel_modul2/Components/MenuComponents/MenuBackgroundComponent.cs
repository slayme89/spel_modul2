
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class MenuBackgroundComponent : IComponent
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public RenderLayer Layer { get; set; }

        public MenuBackgroundComponent(string name, string texturePath, Vector2 position, RenderLayer layer)
        {
            IsActive = false;
            Name = name;
            TexturePath = texturePath;
            Position = position;
            Layer = Layer;
        }
    }
}
