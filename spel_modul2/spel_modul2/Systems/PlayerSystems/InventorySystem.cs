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
                PlayerControlComponent playerComp = (PlayerControlComponent)entity.Value;
                if (invenComp != null)
                {
                    if (playerComp.ActionBar1.IsButtonDown())
                    {
                        AddItemToInventory(entity.Key, 10);
                        
                    }
                    if (playerComp.ActionBar2.IsButtonDown())
                    {
                        AddItemToInventory(entity.Key, 11);

                    }
                    if (playerComp.Inventory.IsButtonDown())
                    {
                        MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                        AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                        if (invenComp.IsOpen)
                        {
                            attackComp.CanAttack = true;
                            moveComp.canMove = true;
                            invenComp.HeldItem = 0;
                            invenComp.IsOpen = false;
                        }
                        else
                        {
                            attackComp.CanAttack = false;
                            moveComp.canMove = false;
                            invenComp.IsOpen = true;
                        }
                    }
                    if (invenComp.IsOpen)
                    {
                        if (invenComp.selectSlotCurCooldown <= 0.0f)
                        {
                            Vector2 stickDir = new Vector2(playerComp.Movement.GetDirection().Y, playerComp.Movement.GetDirection().X);
                            if (Math.Abs(stickDir.X) > 0.5f || Math.Abs(stickDir.Y) > 0.5f)
                            {
                                Point direction = SystemManager.GetInstance().GetSystem<MoveSystem>().CalcDirection(stickDir.X, stickDir.Y);
                                Point nextSlot = invenComp.SelectedSlot + direction;
                                if (nextSlot.X >= 0
                                 && nextSlot.Y >= 0
                                 && nextSlot.X < invenComp.ColumnsRows.X
                                 && nextSlot.Y < invenComp.ColumnsRows.Y)
                                {
                                    invenComp.SelectedSlot = nextSlot;
                                    invenComp.selectSlotCurCooldown = invenComp.SelectSlotDelay;
                                }
                            }
                            if (playerComp.Interact.IsButtonDown())
                            {
                                //calculate the location of the selected slot in the inventory items array
                                int selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                                if (invenComp.HeldItem == 0)
                                {
                                    //Picked an item to hold
                                    invenComp.HeldItem = invenComp.Items[selectedArraySlot];
                                    Debug.WriteLine(invenComp.HeldItem + "   " + (invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X));
                                }else
                                {
                                    ItemComponent heldItemComp = cm.GetComponentForEntity<ItemComponent>(invenComp.HeldItem);
                                    if (invenComp.Items[selectedArraySlot] == 0)
                                    {
                                        //Moved held item
                                        invenComp.Items[selectedArraySlot] = invenComp.HeldItem;
                                        invenComp.Items[heldItemComp.InventoryPosition] = 0;
                                        heldItemComp.InventoryPosition = selectedArraySlot;
                                    }
                                    else
                                    {
                                        //Swap item locations
                                        int itemToSwap = 0;
                                        itemToSwap = invenComp.Items[selectedArraySlot];
                                        invenComp.Items[selectedArraySlot] = invenComp.HeldItem;
                                        invenComp.Items[heldItemComp.InventoryPosition] = itemToSwap;
                                        cm.GetComponentForEntity<ItemComponent>(itemToSwap).InventoryPosition = heldItemComp.InventoryPosition;
                                        heldItemComp.InventoryPosition = selectedArraySlot;
                                    }
                                    invenComp.HeldItem = 0;
                                }
                            }
                        }
                        else
                            invenComp.selectSlotCurCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                            else if (invenComp.Items[column + (invenComp.ColumnsRows.X) * row] != 0)
                                spriteBatch.Draw(t, inventorySlot, Color.Yellow);
                            else
                                spriteBatch.Draw(t, inventorySlot, Color.Gray);
                        }
                    }
                }
            }
            spriteBatch.End();
        }

        public bool AddItemToInventory(int player, int item)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if (!cm.HasEntityComponent<ItemComponent>(item))
            {
                Debug.WriteLine("Trying to add an entity to a players inventory which does not have an ItemComponent");
                return false;
            }
                
            InventoryComponent invenComp = cm.GetComponentForEntity<InventoryComponent>(player);
            for(int invSpot = 0; invSpot < invenComp.Items.Length - 1; invSpot++)
            {
                if (invenComp.Items[invSpot] == 0)
                {
                    invenComp.Items[invSpot] = item;
                    cm.GetComponentForEntity<ItemComponent>(item).InventoryPosition = invSpot;
                    return true;
                }
            }
            //inventory is full
            return false;
        }
    }
}
