using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace GameEngine
{
    class RenderInventorySystem : IRenderSystem, ISystem
    {
        Texture2D t;
        SpriteFont sf;
        public RenderInventorySystem(GraphicsDevice graphicsDevice)
        {
            t = new Texture2D(graphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
        }
        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            spriteBatch.Begin();
            foreach (var entity in cm.GetComponentsOfType<InventoryComponent>())
            {
                InventoryComponent invenComp = (InventoryComponent)entity.Value;

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
                    Rectangle equipmentSlot = new Rectangle();
                    equipmentSlot.Size = invenComp.SlotSize;

                    spriteBatch.Draw(t, equipmentBackground, Color.DarkGray);
                    for (int y = 0; y < invenComp.WeaponBodyHead.Length; y++)
                    {
                        equipmentSlot.Location = new Point(5, 105 - 45 * y) + invenComp.PositionOnScreen;

                        spriteBatch.DrawString(invenComp.font, ((ItemType)y).ToString(), (equipmentSlot.Location - new Point(0, 15)).ToVector2(), Color.Black);

                        if (-y - 1 == invenComp.SelectedSlot.X)
                            spriteBatch.Draw(t, equipmentSlot, Color.Green);
                        else
                            spriteBatch.Draw(t, equipmentSlot, Color.Gray);
                        if (invenComp.WeaponBodyHead[y] != 0)
                        {
                            spriteBatch.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.WeaponBodyHead[y]).ItemIcon, equipmentSlot, Color.Yellow);
                        }
                    }
                    if (cm.HasEntityComponent<StatsComponent>(entity.Key))
                    {
                        StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                        equipmentSlot.Location = new Point(equipmentBackground.Size.X - 55, 0) + invenComp.PositionOnScreen;
                        int statYOffset = 20;
                        string[] statNames = new string[4] {"Str: ", "Agi: ", "Sta: ", "Int: " };
                        int[] statNumbers = new int[4] { statComp.Strength, statComp.Agillity, statComp.Stamina, statComp.Intellect };
                        for(int i = 0; i < 4; i++)
                        {
                            spriteBatch.DrawString(invenComp.font, statNames[i], (equipmentSlot.Location + new Point(0, i * statYOffset + 5)).ToVector2(), Color.Black);
                            spriteBatch.DrawString(invenComp.font, "" + statNumbers[i], (equipmentSlot.Location + new Point(25, i * statYOffset + 5)).ToVector2(), Color.Black);
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
