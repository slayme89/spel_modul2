using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class GUIComponent : IComponent
    {
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Point ScreenPosition{get; set;}
        public RenderLayer Layer { get; set; }

        public GUIComponent(string textureName, Point screenPosition, RenderLayer layer)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            TexturePath = textureName;
            ScreenPosition = screenPosition;
            Layer = layer;

            Texture = rm.GetResource<Texture2D>(textureName);
        }
    }
}
