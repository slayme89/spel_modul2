using System;
using GameEngine.Managers;
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

        public TextureComponent(string textureFilename) : this(textureFilename, RenderLayer.Layer1) { }

        public TextureComponent(string textureFilename, RenderLayer layer)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            TextureFilename = textureFilename;
            Layer = layer;

            Texture = rm.GetResource<Texture2D>(textureFilename);
            Offset = new Point(Texture.Width / 2, Texture.Height / 2);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}