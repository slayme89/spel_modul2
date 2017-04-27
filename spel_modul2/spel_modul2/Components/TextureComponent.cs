using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class TextureComponent : IComponent
    {
        public string textureFilename;
        public Texture2D texture;

        public TextureComponent(string textureFilename)
        {
            this.textureFilename = textureFilename;
        }
    }
}