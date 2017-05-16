using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameEngine
{
    class RenderActionbarSystem : ISystem, IRenderSystem
    {
        private Texture2D texture;

        public RenderActionbarSystem(GraphicsDevice graphicsDevice)
        {
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
        }

        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            foreach (var entity in cm.GetComponentsOfType<ActionBarComponent>())
            {
                ActionBarComponent actionBarComp = (ActionBarComponent)entity.Value;

                int actionBarHeight = (actionBarComp.SlotSize.Y + 10);
                int actionBarWidth = (actionBarComp.SlotSize.Y + 10) * 4;
                actionBarComp.PositionOnScreen = new Point(renderHelper.graphicsDevice.Viewport.TitleSafeArea.Left, renderHelper.graphicsDevice.Viewport.TitleSafeArea.Bottom - actionBarHeight);
                Rectangle actionBarBackground = new Rectangle(actionBarComp.PositionOnScreen, new Point(actionBarWidth, actionBarHeight));

                renderHelper.spriteBatch.Draw(texture, actionBarBackground, Color.Gray);
                for (int slot = 0; slot < 4; slot++)
                        {
                    Rectangle actionBarSlot = new Rectangle(new Point(actionBarComp.PositionOnScreen.X + (actionBarComp.SlotSize.X + 10) * slot + 5, actionBarComp.PositionOnScreen.Y + 5), actionBarComp.SlotSize);
                    renderHelper.spriteBatch.Draw(texture, actionBarSlot, Color.DarkGray);
                }
            }
        }
        public void Update(GameTime gameTime)
        {

        }

    }
}