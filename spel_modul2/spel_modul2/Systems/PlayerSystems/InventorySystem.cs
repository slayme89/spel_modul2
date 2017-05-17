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
                        foreach (var item in cm.GetComponentsOfType<ItemComponent>())
                            AddItemToInventory(entity.Key, item.Key);
                    }

                    if (playerComp.Inventory.IsButtonDown())
                    {
                        if (cm.HasEntityComponent<MoveComponent>(entity.Key) && cm.HasEntityComponent<AttackComponent>(entity.Key))
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
                                //if the stick has been pushed in a direction
                                Point direction = MoveSystem.CalcDirection(stickDir.X, stickDir.Y);
                                Point nextSlot = invenComp.SelectedSlot + direction;

                                UpdateNextSelectedPos(ref nextSlot, invenComp.SelectedSlot);

                                if (UpdateInventoryFocus(invenComp, nextSlot))
                                {
                                    invenComp.SelectedSlot = nextSlot;
                                    invenComp.selectSlotCurCooldown = invenComp.SelectSlotDelay;
                                }
                            }
                            //Selecting slot
                            else if (playerComp.Interact.IsButtonDown())
                            {
                                //calculate the location of the selected slot in the items array
                                int selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                                //if no item is held
                                if (invenComp.HeldItem == 0)
                                {
                                    if (invenComp.LocationInInventory == LocationInInventory.Equipment)
                                    {
                                        //Unequip equipment
                                        int equipPos = Math.Abs(invenComp.SelectedSlot.X) - 1;
                                        if (AddItemToInventory(entity.Key, invenComp.WeaponBodyHead[equipPos]))
                                            invenComp.WeaponBodyHead[equipPos] = 0;
                                    }
                                    else if (invenComp.LocationInInventory == LocationInInventory.Bagspace)
                                        //Picked an item to hold
                                        invenComp.HeldItem = invenComp.Items[selectedArraySlot];
                                    else if (invenComp.LocationInInventory == LocationInInventory.Stats)
                                    {
                                        StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                                        if (statComp.SpendableStats > 0)
                                        {
                                            //Increase the selected stat
                                            if (invenComp.SelectedSlot.X == -1)
                                                //increase int
                                                statComp.AddInt += 1;
                                            else if (invenComp.SelectedSlot.X == -2)
                                                //increase stamina
                                                statComp.AddSta += 1;
                                            else if (invenComp.SelectedSlot.X == -3)
                                                //increase agility
                                                statComp.AddAgi += 1;
                                            else if (invenComp.SelectedSlot.X == -4)
                                                //increase strength
                                                statComp.AddStr += 1;
                                            statComp.SpendableStats--;
                                        }
                                    }
                                }
                                else
                                {
                                    //if we do have a held item
                                    ItemComponent heldItemComp = cm.GetComponentForEntity<ItemComponent>(invenComp.HeldItem);
                                    if (invenComp.LocationInInventory == LocationInInventory.Equipment)
                                    {
                                        //if our currently selected slot is in one of the equipment slots
                                        int equipPos = Math.Abs(invenComp.SelectedSlot.X) - 1;
                                        if ((int)heldItemComp.Type == equipPos)
                                            if (invenComp.WeaponBodyHead[equipPos] == 0)
                                            {
                                                //if there are no items equipped in the selected slot. We equip the held item
                                                invenComp.WeaponBodyHead[equipPos] = invenComp.HeldItem;
                                                invenComp.Items[heldItemComp.InventoryPosition] = 0;
                                                heldItemComp.InventoryPosition = -equipPos;
                                            }
                                            else
                                            {
                                                //if there is an item in the selected slot. Swap locations of the items
                                                int equipToSwap = invenComp.WeaponBodyHead[equipPos];
                                                invenComp.WeaponBodyHead[equipPos] = invenComp.HeldItem;
                                                invenComp.Items[heldItemComp.InventoryPosition] = equipToSwap;
                                                cm.GetComponentForEntity<ItemComponent>(equipToSwap).InventoryPosition = heldItemComp.InventoryPosition;
                                                heldItemComp.InventoryPosition = -equipPos;
                                            }
                                    }
                                    else if (invenComp.LocationInInventory == LocationInInventory.Bagspace)
                                    {
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
                                    }
                                    invenComp.HeldItem = 0; // no matter what action was taken, the held item should be deselected so we can choose a new one in the future
                                }
                            }
                            //Quick equip
                            else if (playerComp.Attack.IsButtonDown())
                            {
                                if (invenComp.LocationInInventory == LocationInInventory.Bagspace)
                                {
                                    int selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                                    ItemComponent selectedItemComp = cm.GetComponentForEntity<ItemComponent>(invenComp.Items[selectedArraySlot]);

                                    if (selectedItemComp != null && (int)selectedItemComp.Type <= 2)
                                    {

                                        if (invenComp.HeldItem != 0)
                                            invenComp.HeldItem = 0;
                                        if (invenComp.WeaponBodyHead[(int)selectedItemComp.Type] == 0)
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
                            else if (cm.HasEntityComponent<ActionBarComponent>(entity.Key))
                            {
                                ActionBarComponent actionBComp = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                                if (playerComp.ActionBar1.IsButtonDown())
                                {

                                }
                                else if (playerComp.ActionBar2.IsButtonDown())
                                {

                                }
                                else if (playerComp.ActionBar3.IsButtonDown())
                                {

                                }
                                else if (playerComp.ActionBar4.IsButtonDown())
                                {

                                }
                                else
                                {
                                    invenComp.selectSlotCurCooldown = 0.0f;
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

        bool UpdateInventoryFocus(InventoryComponent invenComp, Point nextSlot)
        {
            if (nextSlot.X >= 0
             && nextSlot.Y >= 0
             && nextSlot.X < invenComp.ColumnsRows.Y
             && nextSlot.Y < invenComp.ColumnsRows.X)
            {
                //inside bagspace
                invenComp.LocationInInventory = LocationInInventory.Bagspace;
            }
            else if (nextSlot.X <= -1
             && nextSlot.X >= -3
             && nextSlot.Y == 0)
            {
                //inside Equipment
                invenComp.LocationInInventory = LocationInInventory.Equipment;
            }
            else if (nextSlot.X >= -4
             && nextSlot.Y == 1
             && nextSlot.X <= -1)
            {
                //inside stat
                invenComp.LocationInInventory = LocationInInventory.Stats;
            }else if ((nextSlot.Y == 3 && nextSlot.X >= -6)
                || nextSlot.X <=-1
                && nextSlot.X >= -3
                && nextSlot.Y <= 4
                && nextSlot.Y >= 2)
            {
                invenComp.LocationInInventory = LocationInInventory.Skills;
            }
            else
                return false;
            return true;
        }

        void UpdateNextSelectedPos(ref Point nextSlot, Point selectedSlot)
        {
            // Manages special movment between slots. 
            if (selectedSlot.X == 0 && nextSlot.X < 0)
                nextSlot.Y = 0;
            if (selectedSlot.X == -4 && nextSlot.Y == 0)
                nextSlot.X = -3;
            if (selectedSlot.Y == 3) {
                if(nextSlot.Y == 2 || nextSlot.Y == 4)
                    if (selectedSlot.X <= -5)
                        nextSlot.X = -3;
                    else if (selectedSlot.X <= -3)
                        nextSlot.X = -2;
                    else if (selectedSlot.X <= -1)
                        nextSlot.X = -1;
            }else if (selectedSlot.Y == 2 || selectedSlot.Y == 4)
                if (selectedSlot.X == -3 && nextSlot.Y == 3)
                    nextSlot.X = -5;
                else if (selectedSlot.X == -2 && nextSlot.Y == 3)
                    nextSlot.X = -3;
                else if (selectedSlot.X == -1 && nextSlot.Y == 3)
                    nextSlot.X = -1;
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
            for (int invSpot = 0; invSpot < invenComp.Items.Length; invSpot++)
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
