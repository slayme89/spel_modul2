using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class TextureLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var textures = ComponentManager.GetInstance().GetComponentsOfType<TextureComponent>();

            if (textures != null)
            {
                foreach (TextureComponent texture in textures.Values)
                {
                    texture.texture = content.Load<Texture2D>(texture.textureFilename);
                }
            }
        }
    }
}