using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Game.Systems
{
    public class InventorySystem : ISystem
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
                    //if (playerComp.ActionBar1.IsButtonDown())
                    //{
                    //    foreach (var item in cm.GetComponentsOfType<ItemComponent>())
                    //        invenComp.ItemsToAdd.Add(item.Key);
                    //}
                    if (invenComp.ItemsToAdd.Count > 0)
                    {
                        foreach (int item in invenComp.ItemsToAdd.ToArray())
                        {
                            AddItemToInventory(entity.Key, item);
                            invenComp.ItemsToAdd.Remove(item);
                        }
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
                                moveComp.CanMove = true;
                                invenComp.HeldItem = 0;
                                invenComp.IsOpen = false;
                            }
                            else
                            {
                                attackComp.CanAttack = false;
                                moveComp.Velocity = new Vector2(0.0f, 0.0f);
                                moveComp.CanMove = false;
                                invenComp.IsOpen = true;
                            }
                        }
                    }
                    if (invenComp.IsOpen)
                    {
                        if (invenComp.selectSlotCurCooldown <= 0.0f)
                        {
                            Vector2 stickDir = playerComp.Movement.GetDirection();

                            invenComp.selectSlotCurCooldown = invenComp.SelectSlotDelay;
                            if (Math.Abs(stickDir.X) > 0.5f || Math.Abs(stickDir.Y) > 0.5f)
                            {
                                //if the stick has been pushed in a direction
                                Point direction = MoveSystem.CalcDirection(stickDir.Y, stickDir.X);
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
                                        {
                                            UnEquipItemVisually(invenComp.WeaponBodyHead[equipPos], cm);
                                            invenComp.WeaponBodyHead[equipPos] = 0;
                                        }
                                            
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
                                    else if (invenComp.LocationInInventory == LocationInInventory.Skills)
                                    {
                                        StatsComponent statComp = cm.GetComponentForEntity<StatsComponent>(entity.Key);
                                        //Choose the skill selected if it has not already been picked and prerequisite requirements have been met
                                        if (ChooseAvailableSkill(ref invenComp, GetSelectedSkillSlot(invenComp.SelectedSlot.X, invenComp.SelectedSlot.Y)) 
                                            && statComp.SpendableStats >= 5)
                                            statComp.SpendableStats -= 5;
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
                                        {
                                            int equipToSwap = 0;
                                            if (invenComp.WeaponBodyHead[equipPos] != 0)
                                            {
                                                //if there is an item in the selected slot. Swap locations of the items
                                                equipToSwap = invenComp.WeaponBodyHead[equipPos];
                                                cm.GetComponentForEntity<ItemComponent>(equipToSwap).InventoryPosition = heldItemComp.InventoryPosition;
                                                UnEquipItemVisually(equipToSwap, cm);
                                            }
                                            invenComp.WeaponBodyHead[equipPos] = invenComp.HeldItem;
                                            invenComp.Items[heldItemComp.InventoryPosition] = equipToSwap;
                                            heldItemComp.InventoryPosition = -equipPos;
                                        }
                                    }
                                    else if (invenComp.LocationInInventory == LocationInInventory.Bagspace)
                                    {
                                        int itemToSwap = 0;
                                        if (invenComp.Items[selectedArraySlot] != 0)
                                        {
                                            //Swap item locations
                                            itemToSwap = invenComp.Items[selectedArraySlot];
                                            invenComp.Items[heldItemComp.InventoryPosition] = itemToSwap;
                                            cm.GetComponentForEntity<ItemComponent>(itemToSwap).InventoryPosition = heldItemComp.InventoryPosition;
                                            
                                        }
                                        invenComp.Items[selectedArraySlot] = invenComp.HeldItem;
                                        invenComp.Items[heldItemComp.InventoryPosition] = itemToSwap;
                                        heldItemComp.InventoryPosition = selectedArraySlot;
                                    }
                                    invenComp.HeldItem = 0; // no matter what action was taken, the held item should be deselected so we can choose a new one in the future
                                }
                                UpdateActualEquippedItems(ref invenComp, ref cm, entity.Key);
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
                                            UnEquipItemVisually(itemToMove, cm);
                                        }
                                    }
                                }
                                UpdateActualEquippedItems(ref invenComp, ref cm, entity.Key);
                            }
                            else if (cm.HasEntityComponent<ActionBarComponent>(entity.Key))
                            {

                                ActionBarComponent actionBComp = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                                if (playerComp.ActionBar1.IsButtonDown())
                                {
                                    BindToActionBar(ref invenComp, ref actionBComp, ref cm, 0);
                                }
                                else if (playerComp.ActionBar2.IsButtonDown())
                                {
                                    BindToActionBar(ref invenComp, ref actionBComp, ref cm, 1);
                                }
                                else if (playerComp.ActionBar3.IsButtonDown())
                                {
                                    BindToActionBar(ref invenComp, ref actionBComp, ref cm, 2);
                                }
                                else if (playerComp.ActionBar4.IsButtonDown())
                                {
                                    BindToActionBar(ref invenComp, ref actionBComp, ref cm, 3);
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

        void BindToActionBar(ref InventoryComponent invenComp, ref ActionBarComponent actionBComp, ref ComponentManager cm, int slotNumber)
        {

            int selectedArraySlot = 0;
            ActionBarSlotComponent aBSC = null;
            if (invenComp.LocationInInventory == LocationInInventory.Bagspace)
            {
                selectedArraySlot = invenComp.SelectedSlot.Y + (invenComp.ColumnsRows.X) * invenComp.SelectedSlot.X;
                if (invenComp.Items[selectedArraySlot] != 0)
                {
                    aBSC = cm.GetComponentForEntity<ItemComponent>(invenComp.Items[selectedArraySlot]);
                    actionBComp.Slots[slotNumber] = aBSC;
                }
            }
            else if (invenComp.LocationInInventory == LocationInInventory.Skills)
            {
                selectedArraySlot = GetSelectedSkillSlot(invenComp.SelectedSlot.X, invenComp.SelectedSlot.Y);
                if (invenComp.Skills[selectedArraySlot] != 0)
                {
                    aBSC = cm.GetComponentForEntity<SkillComponent>(invenComp.Skills[selectedArraySlot]);
                    actionBComp.Slots[slotNumber] = aBSC;
                }
            }
            for (int i = 0; i < actionBComp.Slots.Length; i++)
            {
                if (i == slotNumber)
                    continue;
                if (actionBComp.Slots[i] == aBSC)
                    actionBComp.Slots[i] = null;
            }
        }

        void UnEquipItemVisually(int entity, ComponentManager cm)
        {
            cm.RemoveComponentFromEntity<PositionComponent>(entity);
        }

        void UpdateActualEquippedItems(ref InventoryComponent invenComp, ref ComponentManager cm, int entity)
        {
            AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity);
            if (invenComp.WeaponBodyHead[0] != 0)
            {
                int weaponID = invenComp.WeaponBodyHead[0];
                if (cm.HasEntityComponent<SwordComponent>(weaponID))
                {
                    attackComp.Type = WeaponType.Sword;
                    attackComp.Damage = cm.GetComponentForEntity<SwordComponent>(weaponID).Damage;
                }
            }
            else if (attackComp.Type != WeaponType.None)
                attackComp.Type = WeaponType.None;
            if (invenComp.WeaponBodyHead[1] != 0)
            {
                int bodyArmorID = invenComp.WeaponBodyHead[1];
                if (cm.HasEntityComponent<ArmorComponent>(bodyArmorID))
                {
                    HealthComponent damageComp = cm.GetComponentForEntity<HealthComponent>(entity);
                    damageComp.DamageReduction[0] = cm.GetComponentForEntity<ArmorComponent>(bodyArmorID).Defence;
                }
            }
            else
                cm.GetComponentForEntity<HealthComponent>(entity).DamageReduction[0] = 0;
            if (invenComp.WeaponBodyHead[2] != 0)
            {
                int headArmorID = invenComp.WeaponBodyHead[2];
                if (cm.HasEntityComponent<ArmorComponent>(headArmorID))
                {
                    HealthComponent damageComp = cm.GetComponentForEntity<HealthComponent>(entity);
                    damageComp.DamageReduction[1] = cm.GetComponentForEntity<ArmorComponent>(headArmorID).Defence;
                }
            }
            else
                cm.GetComponentForEntity<HealthComponent>(entity).DamageReduction[1] = 0;

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
            }
            else if ((nextSlot.Y == 3 && nextSlot.X >= -6)
               || nextSlot.X <= -1
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

        int GetSelectedSkillSlot(int x, int y)
        {
            int selectedSkillSlot = x + y;

            switch (y)
            {
                case 2:
                    return selectedSkillSlot + 1;
                case 3:
                    return selectedSkillSlot + 6;
                case 4:
                    return selectedSkillSlot + 8;
                default:
                    return selectedSkillSlot;
            }
        }

        //returns, true if we managed to pick a new skill, otherwise false
        bool ChooseAvailableSkill(ref InventoryComponent invenComp, int selectedSkillSlot)
        {
            if (invenComp.Skills[selectedSkillSlot] == 0 && invenComp.NotPickedSkills[selectedSkillSlot] != 0)
                switch (selectedSkillSlot)
                {
                    case 3:
                        if (invenComp.Skills[0] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 4:
                        if (invenComp.Skills[0] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 5:
                        if (invenComp.Skills[1] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 6:
                        if (invenComp.Skills[1] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 7:
                        if (invenComp.Skills[2] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 8:
                        if (invenComp.Skills[2] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 9:
                        if (invenComp.Skills[3] != 0 || invenComp.Skills[4] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 10:
                        if (invenComp.Skills[5] != 0 || invenComp.Skills[6] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    case 11:
                        if (invenComp.Skills[7] != 0 || invenComp.Skills[8] != 0)
                            invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                    default:
                        invenComp.Skills[selectedSkillSlot] = invenComp.NotPickedSkills[selectedSkillSlot];
                        return true;
                }
            return false;
        }

        void UpdateNextSelectedPos(ref Point nextSlot, Point selectedSlot)
        {
            // Manages special movment between slots. 
            if (selectedSlot.X == 0 && nextSlot.X < 0)
                nextSlot.Y = 0;
            if (selectedSlot.X == -4 && nextSlot.Y == 0)
                nextSlot.X = -3;
            if (selectedSlot.Y == 3)
            {
                if (nextSlot.Y == 2 || nextSlot.Y == 4)
                    if (selectedSlot.X <= -5)
                        nextSlot.X = -3;
                    else if (selectedSlot.X <= -3)
                        nextSlot.X = -2;
                    else if (selectedSlot.X <= -1)
                        nextSlot.X = -1;
            }
            else if (selectedSlot.Y == 2 || selectedSlot.Y == 4)
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
