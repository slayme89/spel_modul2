using Microsoft.Xna.Framework;
using GameEngine.Systems;

namespace Game.Systems
{
    public class DayNightSystem : ISystem, IRenderSystem
    {
        private double DayDuration = 24;
        private double CurrentTime = 0;
        //Fading stuff
        private int AlphaValue = 0;
        private double Counter = 0;

        public void Update(GameTime gameTime)
        {
            CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentTime >= DayDuration)
                CurrentTime = 0;

            if(CurrentTime >= 12)
            {
                Counter += gameTime.ElapsedGameTime.TotalSeconds;
                if (Counter >= .1)
                {
                    if (CurrentTime <= 18 && AlphaValue < 150)
                        AlphaValue++;
                    else if( AlphaValue > 0)
                        AlphaValue--;
                }
            }
            if (CurrentTime >= 24)
                CurrentTime = 0;
        }

        public void Render(RenderHelper renderHelper)
        {
            Rectangle rect = new Rectangle(
            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Left,
            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Top,
            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Width,
            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Height
            );
            renderHelper.DrawFilledRectangle(
                rect,
                new Color(Color.Black, AlphaValue),
                RenderLayer.ForeGround3);
        }
    }
}


