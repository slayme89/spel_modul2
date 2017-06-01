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
        Texture2D SplashText;
        EntityFactory factory = new EntityFactory();
        SpriteFont Font;

        public void Load(ContentManager content)
        {
            SplashText = content.Load<Texture2D>("Splashscreen/splash");
            Font = content.Load<SpriteFont>("NewSpriteFont");
        }

        public void Update(GameTime gameTime)
        {
            GameStateManager gm = GameStateManager.GetInstance();
            if (gm.State == GameState.Splashscreen)
            {
                if (SecondsSinceLastAlphaIncrease >= 0.01f)
                {
                    if (AlphaVal < 255)
                    {
                        AlphaVal++;
                    }
                    SecondsSinceLastAlphaIncrease = 0;
                }
                SecondsSinceLastAlphaIncrease += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (AlphaVal >= 255)
                    gm.State = GameState.ExitToMenu;
            }
            
        }

        public void Render(RenderHelper renderHelper)
        {
            if (GameStateManager.GetInstance().State == GameState.Splashscreen)
            {
                Rectangle viewBounds = renderHelper.graphicsDevice.Viewport.Bounds;
                Vector2 divideToFitScreen = (SplashText.Bounds.Size.ToVector2() / viewBounds.Size.ToVector2());
                divideToFitScreen.Y = divideToFitScreen.X;
                renderHelper.DrawFilledRectangle(viewBounds, new Color(Color.Black, AlphaVal), RenderLayer.Menubackground);
                if (AlphaVal >= 200)
                {
                    renderHelper.Draw(
                        SplashText,
                        new Rectangle(
                            viewBounds.Location + new Point(0, (viewBounds.Size.Y / 2) - (SplashText.Bounds.Size.Y / 2)),
                            (SplashText.Bounds.Size.ToVector2() / divideToFitScreen).ToPoint()),
                        Color.White,
                        RenderLayer.MenuButton
                    );
                }
            }
        }
    }
}
