using System;
using GameEngine.Systems;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Game.Managers;

namespace Game.Systems
{
    public class RenderSplashscreenSystem : IRenderSystem, ISystem
    {
        int AlphaVal = 0;
        float SecondsSinceLastAlphaIncrease = 0.0f;
        Texture2D SplashTexture;
        EntityFactory factory = new EntityFactory();

        public void Load(ContentManager content)
        {
            SplashTexture = content.Load<Texture2D>("Splashscreen/splash");
        }

        public void Update(GameTime gameTime)
        {
            GameStateManager gm = GameStateManager.GetInstance();
            if (gm.State == GameState.Splashscreen)
            {
                if (SecondsSinceLastAlphaIncrease >= 0.01f)
                {
                    if (AlphaVal < 350)
                    {
                        AlphaVal++;
                    }
                    else
                        gm.State = GameState.ExitToMenu;
                    SecondsSinceLastAlphaIncrease = 0;
                }
                SecondsSinceLastAlphaIncrease += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        public void Render(RenderHelper renderHelper)
        {
            if (GameStateManager.GetInstance().State == GameState.Splashscreen)
            {
                Rectangle viewBounds = renderHelper.graphicsDevice.Viewport.Bounds;
                Vector2 divideToFitScreen = (SplashTexture.Bounds.Size.ToVector2() / viewBounds.Size.ToVector2());
                divideToFitScreen.Y = divideToFitScreen.X;
                renderHelper.DrawFilledRectangle(viewBounds, new Color(Color.Black, AlphaVal), RenderLayer.Menubackground);
                renderHelper.Draw(
                    SplashTexture,
                    new Rectangle(
                        viewBounds.Location + new Point(0, (viewBounds.Size.Y / 2) - (SplashTexture.Bounds.Size.Y / 2)),
                        (SplashTexture.Bounds.Size.ToVector2() / divideToFitScreen).ToPoint()),
                    new Color(255, 255, 255, AlphaVal),
                    RenderLayer.MenuButton
                );
            }
        }
    }
}
