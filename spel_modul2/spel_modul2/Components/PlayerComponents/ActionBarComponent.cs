using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameEngine
{
    class ActionBarComponent : IComponent
    {
        public Point PositionOnScreen { get; set; }
        public Point SlotSize { get; set; }
        public SkillComponent[] Skills { get; set; }
        public ActionBarComponent()
        {
            Skills = new SkillComponent[4];
            SlotSize = new Point(30, 30);
        }
    }
}