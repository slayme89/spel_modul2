using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public enum GUIType { Player1, Player2, Misc};

    public class GUIComponent : IComponent
    {
        public GUIType Type { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Point ScreenPosition {get; set;}
        public RenderLayer Layer { get; set; }
        public bool IsActive { set; get; }

        public GUIComponent(GUIType type, string textureName, Point screenPosition, RenderLayer layer)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Type = type;
            TexturePath = textureName;
            ScreenPosition = screenPosition;
            Layer = layer;
            IsActive = true;
            Texture = rm.GetResource<Texture2D>(textureName);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
