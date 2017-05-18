using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public enum GUIPosition { Left, Right };

    public class GUIComponent : IComponent
    {
        public string TextureName { get; set; }
        public Texture2D Texture { get; set; }
        public GUIPosition ScreenPosition;

        public GUIComponent(string textureName, GUIPosition screenPosition)
        {
            TextureName = textureName;
            ScreenPosition = screenPosition;
        }
    }
}
