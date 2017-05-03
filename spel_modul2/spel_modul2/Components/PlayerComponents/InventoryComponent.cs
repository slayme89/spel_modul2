using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class InventoryComponent : IComponent
    {
        public Point ColumnsRows{ get; set; }
        public Point SlotSize { get; set; }
        public Point PositionOnScreen { get; set; }
        public Point SelectedSlot { get; set; }
        public float SelectSlotDelay { get; set; }
        public float selectSlotCurCooldown { get; set; }
        public int SlotSpace { get; set; }
        public bool IsOpen { get; set; }
        public InventoryComponent(int columns, int rows)
        {
            PositionOnScreen = new Point(10, 100);
            IsOpen = false;
            ColumnsRows = new Point(columns, rows);
            SlotSize = new Point(30, 30);
            SlotSpace = 5;
            SelectSlotDelay = 0.1f;
            selectSlotCurCooldown = 0.0f;
        }
    }
}
