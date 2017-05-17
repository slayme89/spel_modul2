using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameEngine
{
    public delegate void Action();
    class ActionBarComponent : IComponent
    {
        public Point PositionOnScreen { get; set; }
        public Point SlotSize { get; set; }
        public int[] Slots { get; set; }
        public ActionBarComponent()
        {
            Slots = new int[4];
            SlotSize = new Point(30, 30);
        }
    }
}
