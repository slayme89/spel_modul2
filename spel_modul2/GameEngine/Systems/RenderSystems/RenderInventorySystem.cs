using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
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
                    Rectangle inventoryBackground = new Rectangle(itemInventoryPos, new Point(inventoryWidth, inventoryHeight) + invenComp.SlotSpace * invenComp.ColumnsRows);
                    Point selectedSlotSizeAdd = new Point(2, 2);

                    rh.DrawFilledRectangle(inventoryBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int row = 0; row < invenComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < invenComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((invenComp.SlotSize.X + invenComp.SlotSpace.X) * column, (invenComp.SlotSize.Y + invenComp.SlotSpace.Y) * row) + itemInventoryPos + invenComp.SlotSpace, invenComp.SlotSize);


                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0 && invenComp.HeldItem == invenComp.Items[column + (invenComp.ColumnsRows.X) * row])
                            {
                                inventorySlot.Size += selectedSlotSizeAdd;
                                rh.DrawFilledRectangle(inventorySlot, Color.Yellow, RenderLayer.GUI2);
                            } 
                            else if (row == invenComp.SelectedSlot.X && column == invenComp.SelectedSlot.Y)
                            {
                                inventorySlot.Size += selectedSlotSizeAdd;
                                rh.DrawFilledRectangle(inventorySlot, Color.Green, RenderLayer.GUI2);
                            }
                                
                            else
                                rh.DrawFilledRectangle(inventorySlot, Color.Gray, RenderLayer.GUI2);
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                            {
                                rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.Items[column + (invenComp.ColumnsRows.X) * row]).ItemIcon, inventorySlot, Color.Yellow, RenderLayer.GUI3);
                            }
                        }
                    }

                    //Equipment stuff

                    rh.DrawFilledRectangle(equipmentBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int y = 0; y < invenComp.WeaponBodyHead.Length; y++)
                    {
                        Rectangle equipmentSlot = new Rectangle(new Point(5, 105 - 45 * y) + invenComp.PositionOnScreen, invenComp.SlotSize);

                        rh.DrawString(invenComp.font, ((ItemType)y).ToString(), (equipmentSlot.Location - new Point(0, 15)).ToVector2(), Color.Black, RenderLayer.GUI2);

                        if (invenComp.SelectedSlot.X == -y - 1 && invenComp.SelectedSlot.Y <= 0)
                        {
                            equipmentSlot.Size += selectedSlotSizeAdd;
                            rh.DrawFilledRectangle(equipmentSlot, Color.Green, RenderLayer.GUI2);
                        }else
                            rh.DrawFilledRectangle(equipmentSlot, Color.Gray, RenderLayer.GUI2);
                        if (invenComp.WeaponBodyHead[y] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.WeaponBodyHead[y]).ItemIcon, equipmentSlot, Color.Yellow, RenderLayer.GUI3);
                        }
                    }

                    //stats stuff

                    if (cm.HasEntityComponent<StatsComponent>(entity.Key))
                    {
                        
                        StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                        LevelComponent lvlComp = cm.GetComponentForEntity<LevelComponent>(entity.Key);
                        int statYOffset = 25;
                        string[] statNames = new string[4] { "Str: ", "Agi: ", "Sta: ", "Int: " };
                        int[] statNumbers = new int[4] { statComp.Strength, statComp.Agility, statComp.Stamina, statComp.Intellect };
                        Point statPos = new Point(equipmentBackground.Size.X - 55, 0) + invenComp.PositionOnScreen;

                        for (int i = 0; i < 4; i++) // draw all of our stats and statbuttons
                        {
                            Rectangle statButton = new Rectangle(statPos + new Point(40, i * -statYOffset - 6 + 88), new Point(10, 10));
                            rh.DrawString(invenComp.font, statNames[i], (statPos + new Point(0, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.GUI2);
                            rh.DrawString(invenComp.font, "" + statNumbers[i], (statPos + new Point(25, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.GUI2);

                            if (invenComp.SelectedSlot.X == -i - 1 && invenComp.SelectedSlot.Y == 1)
                            {
                                statButton.Size += selectedSlotSizeAdd;
                                rh.DrawFilledRectangle(statButton, Color.Green, RenderLayer.GUI2);
                            }else
                                rh.DrawFilledRectangle(statButton, Color.Gray, RenderLayer.GUI2);
                        }
                        //Specific drawing
                        //Lvl
                        rh.DrawRectangle(new Rectangle((statPos + new Point(-35, 5)), new Point(22, 40)), 2, Color.DarkRed, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, "Lvl", (statPos + new Point(-30, 10)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, lvlComp.CurrentLevel + "", (statPos + new Point(-25, 30)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        //Distributable points
                        rh.DrawString(invenComp.font, "Pts:", (statPos + new Point(0, statYOffset * 5)).ToVector2(), Color.Black, RenderLayer.GUI2);
                        rh.DrawString(invenComp.font, statComp.SpendableStats + "", (statPos + new Point(25, statYOffset * 5)).ToVector2(), Color.Black, RenderLayer.GUI2);

                    }

                    //skill stuff

                    Rectangle skillBackground = new Rectangle(new Point(equipmentBackground.Size.X, 0) + invenComp.PositionOnScreen, new Point(80, equipmentBackground.Size.Y));
                    Point standardSkillSlotSize = new Point(18, 18);
                    int slotSpacing = 2;
                    int yGeneralPos = 99 + slotSpacing / 2;
                    rh.DrawFilledRectangle(skillBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int i = 0; i < 3; i++)
                    {
                        //Three leftmost skills
                        float slotYPos = yGeneralPos - (slotSpacing / 2 + slotSpacing * 2 * i + standardSkillSlotSize.Y * 2 * i);
                        Rectangle skillSlot = new Rectangle(skillBackground.Location + new Point(6, (int)slotYPos), standardSkillSlotSize);
                        



                        if (invenComp.SelectedSlot.X == -i - 1 && invenComp.SelectedSlot.Y == 2)
                        {
                            skillSlot.Size += selectedSlotSizeAdd;
                            rh.DrawFilledRectangle(skillSlot, Color.Green, RenderLayer.GUI2);
                        }
                        else
                            rh.DrawFilledRectangle(skillSlot, Color.Gray, RenderLayer.GUI2);

                        if (invenComp.NotPickedSkills[2 - i] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<SkillComponent>(invenComp.NotPickedSkills[2 - i]).SkillIcon, skillSlot, Color.AliceBlue, RenderLayer.GUI3);
                        }
                        //Three rightmost skills
                        skillSlot = new Rectangle(skillBackground.Location + new Point(18 + standardSkillSlotSize.X * 2, (int)slotYPos), standardSkillSlotSize);

                        if (invenComp.SelectedSlot.X == -i - 1 && invenComp.SelectedSlot.Y == 4)
                        {
                            skillSlot.Size += selectedSlotSizeAdd;
                            rh.DrawFilledRectangle(skillSlot, Color.Green, RenderLayer.GUI2);
                        }
                        else
                            rh.DrawFilledRectangle(skillSlot, Color.Gray, RenderLayer.GUI2);

                        if (invenComp.NotPickedSkills[9 - i] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<SkillComponent>(invenComp.NotPickedSkills[9 - i]).SkillIcon, skillSlot, Color.AliceBlue, RenderLayer.GUI3);
                        }
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        //six middle skills
                        Rectangle skillSlot = new Rectangle(skillBackground.Location + new Point(12 + standardSkillSlotSize.X, yGeneralPos + standardSkillSlotSize.Y / 2 - (slotSpacing + standardSkillSlotSize.Y) * i), standardSkillSlotSize);

                        if (invenComp.SelectedSlot.X == -i - 1 && invenComp.SelectedSlot.Y == 3)
                        {
                            skillSlot.Size += selectedSlotSizeAdd;
                            rh.DrawFilledRectangle(skillSlot, Color.Green, RenderLayer.GUI2);
                        }
                        else
                            rh.DrawFilledRectangle(skillSlot, Color.Gray, RenderLayer.GUI2);

                        if (invenComp.NotPickedSkills[9 - i] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<SkillComponent>(invenComp.NotPickedSkills[8 - i]).SkillIcon, skillSlot, Color.AliceBlue, RenderLayer.GUI3);
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
