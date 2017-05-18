
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class MenuBackgroundComponent : IComponent
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Point Position { get; set; }
        public RenderLayer Layer { get; set; }

        public MenuBackgroundComponent(string name, string texturePath, Point position, RenderLayer layer)
        {
            IsActive = false;
            Name = name;
            TexturePath = texturePath;
            Position = position;
            Layer = Layer;
        }
    }
}
