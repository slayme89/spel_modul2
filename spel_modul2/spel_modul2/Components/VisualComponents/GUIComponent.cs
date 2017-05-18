using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class GUIComponent : IComponent
    {
        public string TextureName { get; set; }
        public Texture2D Texture { get; set; }
        public Point ScreenPosition;

        public GUIComponent(string textureName, Point screenPosition)
        {
            TextureName = textureName;
            ScreenPosition = screenPosition;
        }

        public GUIComponent(string textureName, int xPos, int yPos)
        {
            TextureName = textureName;
            ScreenPosition.X = xPos;
            ScreenPosition.Y = yPos;
        }
    }
}
