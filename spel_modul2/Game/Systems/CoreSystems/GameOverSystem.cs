using GameEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Components;

namespace Game.Systems
{
    class GameOverSystem : ISystem , IRenderSystem
    {
        int AlphaVal = 0;
        float SecondsSinceLastAlphaIncrease = 0.0f;
        Texture2D GameOverText;
        EntityFactory factory = new EntityFactory();
        SpriteFont Font;

        public void Load(ContentManager content)
        {
            GameOverText = content.Load<Texture2D>("GAMEOVERTitle");
            Font = content.Load<SpriteFont>("NewSpriteFont");
        }

        public void Update(GameTime gameTime)
        {
            GameStateManager gm = GameStateManager.GetInstance();
            if (gm.LastState == GameState.GameOver || gm.State == GameState.GameOver && gm.LastState == GameState.Menu)
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
            }
            else if (gm.State == GameState.GameOver && gm.LastState == GameState.Game)
            {
                //ComponentManager.GetInstance().AddEntityWithComponents(factory.CreateMenuController(ControllerType.Keyboard));
                gm.LastState = GameState.GameOver;
            }
        }

        public void Render(RenderHelper renderHelper)
        {
            if (GameStateManager.GetInstance().State == GameState.GameOver)
            {
                Rectangle viewBounds = renderHelper.graphicsDevice.Viewport.Bounds;
                Vector2 divideToFitScreen = (GameOverText.Bounds.Size.ToVector2() / viewBounds.Size.ToVector2());
                divideToFitScreen.Y = divideToFitScreen.X;
                renderHelper.DrawFilledRectangle(viewBounds, new Color(Color.Black, AlphaVal), RenderLayer.Menubackground);
                if(AlphaVal >= 255)
                {
                    renderHelper.Draw(
                        GameOverText,
                        new Rectangle(
                            viewBounds.Location + new Point(0, (viewBounds.Size.Y / 2) - (GameOverText.Bounds.Size.Y / 2)),
                            (GameOverText.Bounds.Size.ToVector2() / divideToFitScreen).ToPoint()),
                        Color.White,
                        RenderLayer.MenuButton
                    );
                    renderHelper.DrawString(Font, "Press 'Esc' for menu options", (viewBounds.Location + new Point(viewBounds.Size.X / 2 - 50, (viewBounds.Size.Y / 2) + 100)).ToVector2() , Color.Red, RenderLayer.MenuButton);
                }
                    
            }
        }
    }
}
