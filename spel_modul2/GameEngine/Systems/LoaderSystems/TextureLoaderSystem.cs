using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    class TextureLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var textures = ComponentManager.GetInstance().GetComponentsOfType<TextureComponent>();

            foreach (TextureComponent texture in textures.Values)
            {
                texture.texture = content.Load<Texture2D>(texture.textureFilename);
                texture.offset = new Point(texture.texture.Width / 2, texture.texture.Height / 2);
            }
        }
    }
}