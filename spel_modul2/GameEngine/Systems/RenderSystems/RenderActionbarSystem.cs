using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    class RenderActionbarSystem : ISystem, IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            foreach (var entity in cm.GetComponentsOfType<ActionBarComponent>())
            {
                ActionBarComponent actionBarComp = (ActionBarComponent)entity.Value;

                int actionBarHeight = (actionBarComp.SlotSize.Y + 10);
                int actionBarWidth = (actionBarComp.SlotSize.Y + 5) * 4 + 5;
                actionBarComp.PositionOnScreen = new Point(renderHelper.graphicsDevice.Viewport.TitleSafeArea.Left, renderHelper.graphicsDevice.Viewport.TitleSafeArea.Bottom - actionBarHeight);
                Rectangle actionBarBackground = new Rectangle(actionBarComp.PositionOnScreen, new Point(actionBarWidth, actionBarHeight));

                renderHelper.DrawFilledRectangle(actionBarBackground, Color.DarkGray, RenderLayer.GUI1);
                for (int slot = 0; slot < 4; slot++)
                {
                    Rectangle actionBarSlot = new Rectangle(new Point(actionBarComp.PositionOnScreen.X + (actionBarComp.SlotSize.X + 5) * slot + 5, actionBarComp.PositionOnScreen.Y + 5), actionBarComp.SlotSize);
                    renderHelper.DrawFilledRectangle(actionBarSlot, Color.Gray, RenderLayer.GUI2);

                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}