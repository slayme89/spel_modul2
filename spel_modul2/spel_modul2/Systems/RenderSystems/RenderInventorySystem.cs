using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class RenderInventorySystem : IRenderSystem, ISystem
    {
        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Texture2D t = new Texture2D(graphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });

            spriteBatch.Begin();
            foreach (var entity in cm.GetComponentsOfType<InventoryComponent>())
            {
                InventoryComponent invenComp = (InventoryComponent)entity.Value;

                if (invenComp.IsOpen)
                {
                    int inventoryWidth = invenComp.ColumnsRows.Y * invenComp.SlotSize.X + invenComp.ColumnsRows.Y;
                    int inventoryHeight = invenComp.ColumnsRows.X * invenComp.SlotSize.Y + invenComp.ColumnsRows.X;
                    spriteBatch.Draw(t, new Rectangle(invenComp.PositionOnScreen, new Point(inventoryWidth, inventoryHeight) + invenComp.SlotSpace * invenComp.ColumnsRows), Color.DarkGray);
                    for (int row = 0; row < invenComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < invenComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((invenComp.SlotSize.X + invenComp.SlotSpace.X) * column, (invenComp.SlotSize.Y + invenComp.SlotSpace.Y) * row) + invenComp.PositionOnScreen + invenComp.SlotSpace, invenComp.SlotSize);

                            if (row == invenComp.SelectedSlot.X && column == invenComp.SelectedSlot.Y)
                                spriteBatch.Draw(t, inventorySlot, Color.Green);
                            else
                                spriteBatch.Draw(t, inventorySlot, Color.Gray);
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                                spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.Items[column + (invenComp.ColumnsRows.X) * row]).ItemIcon, inventorySlot, Color.Yellow);
                        }
                    }
                }
            }
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
