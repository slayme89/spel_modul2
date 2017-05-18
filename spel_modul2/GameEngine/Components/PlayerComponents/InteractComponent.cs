using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public enum InteractType { Trap, Talk };

    public class InteractComponent : IComponent
    {
        public InteractType Type { get; set; }
        public string Text { get; set; }
        public int Damage { get; set; }
        public bool IsActive { get; set; }
        public Vector2 Position { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string SpriteFontPath { get; set; }

        public InteractComponent(InteractType type, int damage)
        {
            Type = type;
            Damage = damage;
            IsActive = false;
        }

        public InteractComponent(InteractType type, string text, string spriteFontPath, Vector2 position)
        {
            Type = type;
            Text = text;
            IsActive = false;
            SpriteFontPath = spriteFontPath;
            Position = position;
        }
    }
}
