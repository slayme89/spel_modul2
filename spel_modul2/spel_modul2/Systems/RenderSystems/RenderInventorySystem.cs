using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace GameEngine
{
    class RenderInventorySystem : ISystem, IRenderSystem
    {
        public void Render(RenderHelper rh)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            
            foreach (var entity in cm.GetComponentsOfType<InventoryComponent>())
            {
                InventoryComponent invenComp = (InventoryComponent)entity.Value;

                if (invenComp.IsOpen)
                {
                    Rectangle equipmentBackground = new Rectangle(invenComp.PositionOnScreen, new Point(120, 120) + invenComp.SlotSpace * invenComp.ColumnsRows);
                    //Inventory stuff
                    Point itemInventoryPos = new Point(invenComp.PositionOnScreen.X, invenComp.PositionOnScreen.Y + equipmentBackground.Height);
                    int inventoryHeight = invenComp.ColumnsRows.Y * invenComp.SlotSize.X + invenComp.ColumnsRows.Y;
                    int inventoryWidth = invenComp.ColumnsRows.X * invenComp.SlotSize.Y + invenComp.ColumnsRows.X;
                    Rectangle inventoryBackground = new Rectangle(itemInventoryPos, new Point(inventoryWidth, inventoryHeight) + invenComp.SlotSpace * invenComp.ColumnsRows );
                    
                    rh.DrawFilledRectangle(inventoryBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int row = 0; row < invenComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < invenComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((invenComp.SlotSize.X + invenComp.SlotSpace.X) * column, (invenComp.SlotSize.Y + invenComp.SlotSpace.Y) * row) + itemInventoryPos + invenComp.SlotSpace, invenComp.SlotSize);

                            
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0 && invenComp.HeldItem == invenComp.Items[column + (invenComp.ColumnsRows.X) * row])
                                rh.DrawFilledRectangle(inventorySlot, Color.Yellow, RenderLayer.GUI2);
                            else if (row == invenComp.SelectedSlot.X && column == invenComp.SelectedSlot.Y)
                                rh.DrawFilledRectangle(inventorySlot, Color.Green, RenderLayer.GUI2);
                            else
                                rh.DrawFilledRectangle(inventorySlot, Color.Gray, RenderLayer.GUI2);
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                            {
                                rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.Items[column + (invenComp.ColumnsRows.X) * row]).ItemIcon, inventorySlot, Color.Yellow, RenderLayer.GUI3);
                            }
                        }
                    }

                    //Equipment stuff
                    Rectangle equipmentSlot = new Rectangle();
                    equipmentSlot.Size = invenComp.SlotSize;
                    
                    rh.DrawFilledRectangle(equipmentBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int y = 0; y < invenComp.WeaponBodyHead.Length; y++)
                    {
                        equipmentSlot.Location = new Point(5, 105 - 45 * y) + invenComp.PositionOnScreen;

                        rh.DrawString(invenComp.font, ((ItemType)y).ToString(), (equipmentSlot.Location - new Point(0, 15)).ToVector2(), Color.Black, RenderLayer.GUI2);

                        if (invenComp.SelectedSlot.X == - y - 1 && invenComp.SelectedSlot.Y <= 0)
                            rh.DrawFilledRectangle(equipmentSlot, Color.Green, RenderLayer.GUI2);
                        else
                            rh.DrawFilledRectangle(equipmentSlot, Color.Gray, RenderLayer.GUI2);
                        if (invenComp.WeaponBodyHead[y] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.WeaponBodyHead[y]).ItemIcon, equipmentSlot, Color.Yellow, RenderLayer.GUI3);
                        }
                    }
                    if (cm.HasEntityComponent<StatsComponent>(entity.Key))
                    {
                        //stats stuff
                        StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                        LevelComponent lvlComp = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        int statYOffset = 20;
                        string[] statNames = new string[4] {"Str: ", "Agi: ", "Sta: ", "Int: " };
                        int[] statNumbers = new int[4] { statComp.Strength, statComp.Agillity, statComp.Stamina, statComp.Intellect };
                        Rectangle statButton = new Rectangle(new Point(), new Point(10, 10));

                        equipmentSlot.Location = new Point(equipmentBackground.Size.X - 55, 0) + invenComp.PositionOnScreen;
                        for (int i = 0; i < 4; i++) // draw all of our stats and statbuttons
                        {
                            statButton.Location = equipmentSlot.Location + new Point(40, i * -statYOffset - 6 + 72);
                            rh.DrawString(invenComp.font, statNames[i], (equipmentSlot.Location + new Point(0, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.GUI2);
                            rh.DrawString(invenComp.font, "" + statNumbers[i], (equipmentSlot.Location + new Point(25, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.GUI2);

                            if (invenComp.SelectedSlot.X == -i -1 && invenComp.SelectedSlot.Y >= 1)
                                rh.DrawFilledRectangle(statButton, Color.Green, RenderLayer.GUI2);
                            else
                                rh.DrawFilledRectangle(statButton, Color.Gray, RenderLayer.GUI2);
                        }

                        //Specific drawing
                        //Lvl
                        rh.DrawRectangle(new Rectangle((equipmentSlot.Location + new Point(-35, 5)), new Point(22, 40)), 2, Color.DarkRed, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, "Lvl", (equipmentSlot.Location + new Point(-30, 10)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, lvlComp.CurrentLevel + "", (equipmentSlot.Location + new Point(-25, 30)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        //Distributable points
                        rh.DrawString(invenComp.font, "Pts:", (equipmentSlot.Location + new Point(0, statYOffset * 5)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, statComp.SpendableStats + "", (equipmentSlot.Location + new Point(25, statYOffset * 5)).ToVector2(), Color.Black, RenderLayer.GUI2);

                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
