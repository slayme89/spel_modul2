using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameEngine
{
    class InventorySystem : ISystem
    {
        Texture2D t;
        public InventorySystem(GraphicsDevice gd)
        {
            t = new Texture2D(gd, 1, 1);
            t.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                InventoryComponent invenComp = cm.GetComponentForEntity<InventoryComponent>(entity.Key);
                if (invenComp != null)
                {
                    if (((PlayerControlComponent)entity.Value).Inventory.IsButtonDown())
                    {
                        if (invenComp.IsOpen)
                            invenComp.IsOpen = false;
                        else
                            invenComp.IsOpen = true;
                    }
                    if (invenComp.IsOpen)
                    {
                        Vector2 stickDir = new Vector2(((PlayerControlComponent)entity.Value).Movement.GetDirection().Y, ((PlayerControlComponent)entity.Value).Movement.GetDirection().X);
                        if (Math.Abs(stickDir.X) > 0.5f || Math.Abs(stickDir.Y) > 0.5f)
                        {
                            Point direction = SystemManager.GetInstance().GetSystem<MoveSystem>().CalcDirection(stickDir.X, stickDir.Y);
                            Point nextSlot = invenComp.SelectedSlot + direction;
                            Debug.WriteLine(nextSlot + " " + invenComp.SelectedSlot);
                            if (nextSlot.X >= 0
                             && nextSlot.Y >= 0
                             && nextSlot.X < invenComp.ColumnsRows.X
                             && nextSlot.Y < invenComp.ColumnsRows.Y)
                            {
                                invenComp.SelectedSlot = nextSlot;
                            }
                        }
                    }
                }
            }
        }

        public void Render(GraphicsDevice graphicsDeive, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<InventoryComponent>())
            {
                InventoryComponent inventComp = (InventoryComponent)entity.Value;
                if(inventComp.IsOpen)
                    for(int row = 0; row < inventComp.ColumnsRows.Y; row++)
                    {
                        for(int column = 0; column < inventComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((inventComp.SlotSpace + inventComp.SlotSize.X) * column, (inventComp.SlotSpace + inventComp.SlotSize.Y) * row) + inventComp.PositionOnScreen, inventComp.SlotSize);
                            if(row == inventComp.SelectedSlot.X && column == inventComp.SelectedSlot.Y)
                                spriteBatch.Draw(t, inventorySlot, Color.Green);
                            else
                                spriteBatch.Draw(t, inventorySlot, Color.Black);
                        }
                    }
            }
            spriteBatch.End();
        }
    }
}
