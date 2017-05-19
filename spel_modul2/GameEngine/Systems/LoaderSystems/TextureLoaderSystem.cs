using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class TextureLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var textures = ComponentManager.GetInstance().GetComponentsOfType<TextureComponent>();

            foreach (TextureComponent texture in textures.Values)
            {
                texture.Texture = content.Load<Texture2D>(texture.TextureFilename);
                texture.Offset = new Point(texture.Texture.Width / 2, texture.Texture.Height / 2);
            }
        }
    }
}