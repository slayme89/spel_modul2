using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameEngine
{
    class ActionBarComponent : IComponent
    {
        public bool actionbarOpen { get; set; }
        public Point point { get; set; }
        public Point positionOnScreen { get; set; }
        public Point slotSize { get; set; }
        public Point slotSpace { get; set; }
        public int[] Items { get; set; }
        public Point ColumnsRows { get; set; }
        public Point selectedSlot { get; set; }
        public float selectSlotCurCooldown { get; set; }
        public Point InventoryItemPos { get; set; }
        public float selectedSlotDelay { get; set; }
        public ActionBarComponent(int columns, int rows)
        {
            Items = new int[columns * rows];
            positionOnScreen = new Point(0, 80);
            actionbarOpen = false;
            ColumnsRows = new Point(columns, rows);
            slotSize = new Point(30, 30);
            slotSpace = new Point(5, 5);
        }
    }
}