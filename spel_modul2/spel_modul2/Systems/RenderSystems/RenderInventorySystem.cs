using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace GameEngine
{
    class RenderInventorySystem : ISystem, IRenderSystem
    {
        Texture2D t;
        //SpriteFont sf;
        public RenderInventorySystem(GraphicsDevice graphicsDevice)
        {
            t = new Texture2D(graphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
        }
        public void Render(RenderHelper rh)
        {
            //SpriteBatch spriteBatch = rh.spriteBatch;
            ComponentManager cm = ComponentManager.GetInstance();
            
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

                    rh.Draw(t, inventoryBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int row = 0; row < invenComp.ColumnsRows.Y; row++)
                    {
                        for (int column = 0; column < invenComp.ColumnsRows.X; column++)
                        {
                            Rectangle inventorySlot = new Rectangle(new Point((invenComp.SlotSize.X + invenComp.SlotSpace.X) * column, (invenComp.SlotSize.Y + invenComp.SlotSpace.Y) * row) + itemInventoryPos + invenComp.SlotSpace, invenComp.SlotSize);

                            if (row == invenComp.SelectedSlot.X && column == invenComp.SelectedSlot.Y)
                                rh.Draw(t, inventorySlot, Color.Green, RenderLayer.GUI2);
                            else
                                rh.Draw(t, inventorySlot, Color.Gray, RenderLayer.GUI2);
                            if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                            {
                                if(invenComp.HeldItem == invenComp.Items[column + (invenComp.ColumnsRows.X) * row])
                                    rh.Draw(t, inventorySlot, Color.Yellow, RenderLayer.GUI2);
                                rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.Items[column + (invenComp.ColumnsRows.X) * row]).ItemIcon, inventorySlot, Color.Yellow, RenderLayer.GUI2);
                            }
                        }
                    }
                    Rectangle equipmentSlot = new Rectangle();
                    equipmentSlot.Size = invenComp.SlotSize;

                    rh.Draw(t, equipmentBackground, Color.DarkGray, RenderLayer.GUI1);
                    for (int y = 0; y < invenComp.WeaponBodyHead.Length; y++)
                    {
                        equipmentSlot.Location = new Point(5, 105 - 45 * y) + invenComp.PositionOnScreen;

                        rh.DrawString(invenComp.font, ((ItemType)y).ToString(), (equipmentSlot.Location - new Point(0, 15)).ToVector2(), Color.Black, RenderLayer.GUI2);

                        if (-y - 1 == invenComp.SelectedSlot.X)
                            rh.Draw(t, equipmentSlot, Color.Green, RenderLayer.GUI2);
                        else
                            rh.Draw(t, equipmentSlot, Color.Gray, RenderLayer.GUI2);
                        if (invenComp.WeaponBodyHead[y] != 0)
                        {
                            rh.Draw(cm.GetComponentForEntity<ItemComponent>(invenComp.WeaponBodyHead[y]).ItemIcon, equipmentSlot, Color.Yellow, RenderLayer.GUI2);
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
                            rh.DrawString(invenComp.font, statNames[i], (equipmentSlot.Location + new Point(0, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.Foreground2);
                            rh.DrawString(invenComp.font, "" + statNumbers[i], (equipmentSlot.Location + new Point(25, i * statYOffset + 5)).ToVector2(), Color.Black, RenderLayer.Foreground2);
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
