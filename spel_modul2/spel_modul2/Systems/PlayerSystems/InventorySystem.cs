using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine
{
    class InventorySystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerComp = (PlayerControlComponent)entity.Value;
                
                if (cm.HasEntityComponent<InventoryComponent>(entity.Key))
                {
                    InventoryComponent invenComp = cm.GetComponentForEntity<InventoryComponent>(entity.Key);
                    //Test to add item press 1 on the keyboard or rt + a on gamepad
                    if (playerComp.ActionBar1.IsButtonDown())
                    {
                        foreach(var item in cm.GetComponentsOfType<ItemComponent>())
                            AddItemToInventory(entity.Key, item.Key);
                    }

                    if (playerComp.Inventory.IsButtonDown())
                    {
                        if(cm.HasEntityComponent<MoveComponent>(entity.Key) && cm.HasEntityComponent<AttackComponent>(entity.Key))
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
                    }
                    if (invenComp.IsOpen)
                    {
                        if (invenComp.selectSlotCurCooldown <= 0.0f)
                        {
                            Vector2 stickDir = new Vector2(playerComp.Movement.GetDirection().Y, playerComp.Movement.GetDirection().X);

                            invenComp.selectSlotCurCooldown = invenComp.SelectSlotDelay;
                            if (Math.Abs(stickDir.X) > 0.5f || Math.Abs(stickDir.Y) > 0.5f)
                            {
                                Point direction = SystemManager.GetInstance().GetSystem<MoveSystem>().CalcDirection(stickDir.X, stickDir.Y);
                                Point nextSlot = invenComp.SelectedSlot + direction;
                                if (nextSlot.X >= -3
                                 && nextSlot.Y >= 0
                                 && nextSlot.X < invenComp.ColumnsRows.Y
                                 && nextSlot.Y < invenComp.ColumnsRows.X)
                                {
                                    invenComp.SelectedSlot = nextSlot;
                                    invenComp.selectSlotCurCooldown = invenComp.SelectSlotDelay;
                                }
                            }
                            else if (playerComp.Interact.IsButtonDown())
                            {
                                //calculate the location of the selected slot in the inventory items array
                                int selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                                if (invenComp.HeldItem == 0)
                                {
                                    if (invenComp.SelectedSlot.X < 0)
                                    {
                                        int equipPos = Math.Abs(invenComp.SelectedSlot.X) - 1;
                                        if (AddItemToInventory(entity.Key, invenComp.WeaponBodyHead[equipPos]))
                                            invenComp.WeaponBodyHead[equipPos] = 0;
                                    }else
                                        //Picked an item to hold
                                        invenComp.HeldItem = invenComp.Items[selectedArraySlot];
                                    //Debug.WriteLine(invenComp.HeldItem + "   " + (invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X));
                                }else
                                {
                                    ItemComponent heldItemComp = cm.GetComponentForEntity<ItemComponent>(invenComp.HeldItem);
                                    if (invenComp.SelectedSlot.X < 0)
                                    {
                                        int equipPos = Math.Abs(invenComp.SelectedSlot.X) - 1;
                                        if((int)heldItemComp.Type == equipPos)
                                            if (invenComp.WeaponBodyHead[equipPos] == 0)
                                            {
                                                invenComp.WeaponBodyHead[equipPos] = invenComp.HeldItem;
                                                invenComp.Items[heldItemComp.InventoryPosition] = 0;
                                                heldItemComp.InventoryPosition = -equipPos;
                                            }
                                            else
                                            {
                                                int equipToSwap = invenComp.WeaponBodyHead[equipPos];
                                                invenComp.WeaponBodyHead[equipPos] = invenComp.HeldItem;
                                                invenComp.Items[heldItemComp.InventoryPosition] = equipToSwap;
                                                cm.GetComponentForEntity<ItemComponent>(equipToSwap).InventoryPosition = heldItemComp.InventoryPosition;
                                                heldItemComp.InventoryPosition = -equipPos;
                                            }
                                    }
                                    else if (invenComp.Items[selectedArraySlot] == 0)
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
                            else if (playerComp.Attack.IsButtonDown())
                            {
                                if(invenComp.SelectedSlot.X >= 0)
                                {
                                    int selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                                    ItemComponent selectedItemComp = cm.GetComponentForEntity<ItemComponent>(invenComp.Items[selectedArraySlot]);

                                    if (selectedItemComp != null && (int)selectedItemComp.Type <= 2)
                                    {
                                        //Debug.WriteLine("item is equippable");
                                        if(invenComp.WeaponBodyHead[(int)selectedItemComp.Type] == 0)
                                        {
                                            invenComp.WeaponBodyHead[(int)selectedItemComp.Type] = invenComp.Items[selectedArraySlot];
                                            selectedItemComp.InventoryPosition = -(int)selectedItemComp.Type - 1;
                                            invenComp.Items[selectedArraySlot] = 0;
                                        }
                                        else
                                        {
                                            int itemToMove = invenComp.WeaponBodyHead[(int)selectedItemComp.Type];
                                            invenComp.WeaponBodyHead[(int)selectedItemComp.Type] = invenComp.Items[selectedArraySlot];
                                            invenComp.Items[selectedArraySlot] = itemToMove;

                                            cm.GetComponentForEntity<ItemComponent>(itemToMove).InventoryPosition = selectedItemComp.InventoryPosition;
                                            selectedItemComp.InventoryPosition = -(int)selectedItemComp.Type - 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invenComp.selectSlotCurCooldown = 0.0f;
                            }
                        }
                        else
                            invenComp.selectSlotCurCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
        }

        public bool AddItemToInventory(int player, int item)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            ItemComponent itemComp = cm.GetComponentForEntity<ItemComponent>(item);
            if (itemComp == null)
            {
                //Debug.WriteLine("Trying to add an entity to a players inventory which does not have an ItemComponent or does not exist");
                return false;
            }
            InventoryComponent invenComp = cm.GetComponentForEntity<InventoryComponent>(player);
            for(int invSpot = 0; invSpot < invenComp.Items.Length; invSpot++)
            {
                if (invenComp.Items[invSpot] == 0)
                {
                    invenComp.Items[invSpot] = item;
                    itemComp.InventoryPosition = invSpot;
                    return true;
                }
            }
            //inventory is full
            return false;
        }
    }
}
