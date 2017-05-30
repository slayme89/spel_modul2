using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public enum TextType { DialogBox };
    public class TextComponent : IComponent
    {
        public bool IsActive { get; set; }
        public Vector2 Position { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string SpriteFontPath { get; set; }
        public string Text { get; set; }
        public Color Color;
        public float Duration { get; set; }
        public TextType Type { get; set; }

        public TextComponent(string path, string text, Vector2 position, Color color, bool isActive)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Position = position;
            SpriteFontPath = path;
            Text = text;
            Color = color;
            IsActive = isActive;
            Duration = 4.0f;
            SpriteFont = rm.GetResource<SpriteFont>(path);
        }

        public TextComponent(string path, string text, Color color, TextType type)
        {
            ResourceManager rm = ResourceManager.GetInstance();
            Type = type;
            SpriteFontPath = path;
            Text = text;
            Color = color;
            IsActive = false;
            Duration = 4.0f;
            SpriteFont = rm.GetResource<SpriteFont>(path);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
