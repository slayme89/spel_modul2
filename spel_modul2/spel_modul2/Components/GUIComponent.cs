using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class GUIComponent : IComponent
    {
        public Texture2D Texture { get; set; }
        public Point ScreenPosition { get; set; }

        public GUIComponent(Texture2D texture, Point screenPosition)
        {
            Texture = texture;
            ScreenPosition = screenPosition;
        }
    }
}
