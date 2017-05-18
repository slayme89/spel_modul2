using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class TextureComponent : IComponent
    {
        public string TextureFilename;
        public Texture2D Texture;
        public Point Offset;
        public RenderLayer Layer;

        public TextureComponent(string textureFilename)
        {
            TextureFilename = textureFilename;
            Layer = RenderLayer.Layer1;
        }

        public TextureComponent(string textureFilename, RenderLayer layer)
        {
            TextureFilename = textureFilename;
            Layer = layer;
        }

        public TextureComponent(string textureFilename, Point offset, RenderLayer layer)
        {
            TextureFilename = textureFilename;
            Offset = offset;
            Layer = layer;
        }
    }
}