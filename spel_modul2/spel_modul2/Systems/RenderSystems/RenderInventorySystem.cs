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
                EquipmentComponent equipComp = cm.GetComponentForEntity<EquipmentComponent>(entity.Key);

                if (invenComp.IsOpen)
                {
                    Rectangle equipmentBackground = new Rectangle(invenComp.PositionOnScreen, new Point(120, 120) + invenComp.SlotSpace * invenComp.ColumnsRows);
                    Point itemInventoryPos = new Point(invenComp.PositionOnScreen.X, invenComp.PositionOnScreen.Y + equipmentBackground.Height);
                    int inventoryHeight = invenComp.ColumnsRows.Y * invenComp.SlotSize.X + invenComp.ColumnsRows.Y;
                    int inventoryWidth = invenComp.ColumnsRows.X * invenComp.SlotSize.Y + invenComp.ColumnsRows.X;
                    Rectangle inventoryBackground = new Rectangle(itemInventoryPos, new Point(inventoryWidth, inventoryHeight) + invenComp.SlotSpace * invenComp.ColumnsRows );

                    
                    spriteBatch.Draw(t, inventoryBackground, Color.DarkGray);
                    for (int row = 0; row < invenComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < invenComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((invenComp.SlotSize.X + invenComp.SlotSpace.X) * column, (invenComp.SlotSize.Y + invenComp.SlotSpace.Y) * row) + itemInventoryPos + invenComp.SlotSpace, invenComp.SlotSize);

                            if (row == invenComp.SelectedSlot.X && column == invenComp.SelectedSlot.Y)
                                spriteBatch.Draw(t, inventorySlot, Color.Green);
                            else
                                spriteBatch.Draw(t, inventorySlot, Color.Gray);
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                            {
                                if(invenComp.HeldItem == invenComp.Items[column + (invenComp.ColumnsRows.X) * row])
                                    spriteBatch.Draw(t, inventorySlot, Color.Yellow);
                                spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.Items[column + (invenComp.ColumnsRows.X) * row]).ItemIcon, inventorySlot, Color.Yellow);
                            }
                        }
                    }
                    if(equipComp != null)
                    {
                        Point HeadPos = new Point(5, 15) + invenComp.PositionOnScreen;
                        Point BodyPos = new Point(5, 60) + invenComp.PositionOnScreen;
                        Point WeaponPos = new Point(5, 105) + invenComp.PositionOnScreen;
                        Rectangle equipmentSlot = new Rectangle(HeadPos, invenComp.SlotSize);

                        spriteBatch.Draw(t, equipmentBackground, Color.DarkGray);
                        if (invenComp.SelectedSlot.X == -3)
                            spriteBatch.Draw(t, equipmentSlot, Color.Green);
                        else
                            spriteBatch.Draw(t, equipmentSlot, Color.Gray);
                        equipmentSlot.Location = BodyPos;
                        if (invenComp.SelectedSlot.X == -2)
                            spriteBatch.Draw(t, equipmentSlot, Color.Green);
                        else
                            spriteBatch.Draw(t, equipmentSlot, Color.Gray);
                            equipmentSlot.Location = WeaponPos;
                        if (invenComp.SelectedSlot.X == -1)
                            spriteBatch.Draw(t, equipmentSlot, Color.Green);
                        else
                            spriteBatch.Draw(t, equipmentSlot, Color.Gray);
                        if (equipComp.Head != 0)
                        {
                            equipmentSlot.Location = HeadPos;
                            spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(equipComp.Head).ItemIcon, equipmentSlot, Color.Yellow);
                        }
                        if(equipComp.Body != 0)
                        {
                            equipmentSlot.Location = BodyPos;
                            spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(equipComp.Body).ItemIcon, equipmentSlot, Color.Yellow);
                        }
                        if(equipComp.Weapon != 0)
                        {
                            equipmentSlot.Location = WeaponPos;
                            spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(equipComp.Weapon).ItemIcon, equipmentSlot, Color.Yellow);
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
