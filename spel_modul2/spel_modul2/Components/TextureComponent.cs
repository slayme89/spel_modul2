using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    /*
     * Width - texture width.
     * Height - texture height.
     * Position - start position for the texture.
     * Rotation - rotation of the texture.
     * Sprite - content loaded from the contentmanager(Content/Content.mgcb).
     * 
     */
    class TextureComponent
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Position { get; set; }
        public float Rotation { get; set; }
        public Texture2D Sprite { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Rectangle DestinationRec { get; set; }
    }
}
