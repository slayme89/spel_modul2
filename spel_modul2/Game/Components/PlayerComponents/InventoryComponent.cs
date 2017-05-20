using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using GameEngine.Components;

namespace Game.Components
{
    public enum LocationInInventory
    {
        Bagspace,
        Equipment,
        Skills,
        Stats
    }

    public class InventoryComponent : IComponent
    {
        public int[] Items { get; set; }
        public List<int> ItemsToAdd = new List<int>();
        public Point ColumnsRows { get; set; }
        public Point SlotSize { get; set; }
        public Point PositionOnScreen { get; set; }
        public Point SelectedSlot { get; set; }
        public float SelectSlotDelay { get; set; }
        public float selectSlotCurCooldown { get; set; }
        public Point SlotSpace { get; set; }
        public bool IsOpen { get; set; }
        public int HeldItem { get; set; }
        public int[] WeaponBodyHead = new int[3];
        public int[] Skills = new int[12];
        public int[] NotPickedSkills = new int[12];
        public SpriteFont Font { get; set; }
        public LocationInInventory LocationInInventory;

        public InventoryComponent(int columns, int rows)
        {
            HeldItem = 0;
            Items = new int[columns * rows];
            PositionOnScreen = new Point(10, 100);
            IsOpen = false;
            ColumnsRows = new Point(columns, rows);
            SlotSize = new Point(30, 30);
            SlotSpace = new Point(5, 5);
            SelectSlotDelay = 0.15f;
            selectSlotCurCooldown = 0.0f;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
