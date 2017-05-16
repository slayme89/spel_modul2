using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class RenderActionbarSystem : ISystem, IRenderSystem
    {
        public void Render(RenderHelper rh)
        {
            GraphicsDevice graphicsDevice = rh.graphicsDevice;
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<ActionBarComponent>())
            {
                ActionBarComponent actionBarComp = (ActionBarComponent)entity.Value;
                if (actionBarComp.actionbarOpen)
                {
                    int actionBarHeight = actionBarComp.ColumnsRows.Y * actionBarComp.slotSize.X + actionBarComp.ColumnsRows.Y;
                    int actionBarWidth = actionBarComp.ColumnsRows.X * actionBarComp.slotSize.Y + actionBarComp.ColumnsRows.X;
                    Point itemPos = new Point((int)(graphicsDevice.Viewport.Width * 0.5 - actionBarWidth * 0.5), graphicsDevice.Viewport.Height - actionBarHeight);
                    Rectangle actionBarBackground = new Rectangle(itemPos, new Point(actionBarWidth, actionBarHeight) + actionBarComp.slotSpace * actionBarComp.ColumnsRows);
                    
                    rh.DrawFilledRectangle(actionBarBackground, Color.DarkRed, RenderLayer.GUI1);
                    for (int row = 0; row < actionBarComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < actionBarComp.ColumnsRows.X; column++)
                        {
                            Rectangle actionBarSlot = new Rectangle(new Point((actionBarComp.slotSize.X + actionBarComp.slotSpace.X) * column, (actionBarComp.slotSize.Y + actionBarComp.slotSpace.Y) * row) + itemPos + actionBarComp.slotSpace, actionBarComp.slotSize);

                            if (row == actionBarComp.selectedSlot.X && column == actionBarComp.selectedSlot.Y)
                                rh.DrawFilledRectangle(actionBarSlot, Color.Green, RenderLayer.GUI2);
                            else
                                rh.DrawFilledRectangle(actionBarSlot, Color.OrangeRed, RenderLayer.GUI2);
                        }
                    }
                }

            }
        }
        public void Update(GameTime gameTime)
        {

        }

    }
}