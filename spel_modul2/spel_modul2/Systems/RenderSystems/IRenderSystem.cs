using Microsoft.Xna.Framework.Graphics;
namespace GameEngine
{
    public interface IRenderSystem
    {
        void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch);
    }

}
