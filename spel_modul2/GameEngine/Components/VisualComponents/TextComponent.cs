using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class TextComponent : IComponent
    {
        public bool IsActive { get; set; }
        public Vector2 Position { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string SpriteFontPath { get; set; }
        public string Text { get; set; }
        public Color Color;

        public TextComponent(string path, string text, Vector2 position, Color color, bool isActive)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            Position = position;
            SpriteFontPath = path;
            Text = text;
            Color = color;
            IsActive = isActive;

            SpriteFont = rm.GetResource<SpriteFont>(path);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
