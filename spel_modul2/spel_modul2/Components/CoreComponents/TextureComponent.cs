using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class TextureComponent : IComponent
    {
        public string textureFilename;
        public Texture2D texture;
        public Point offset;
        public RenderLayer layer;

        public TextureComponent(string textureFilename)
        {
            this.textureFilename = textureFilename;
            layer = RenderLayer.Layer1;
        }

        public TextureComponent(string textureFilename, RenderLayer layer)
        {
            this.textureFilename = textureFilename;
            this.layer = layer;
        }
    }
}