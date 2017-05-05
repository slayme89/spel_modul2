using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameEngine
{
    class ActionBarComponent : IComponent
    {
        public Texture2D actionBox1 { get; set; }
        public Texture2D actionBox2 { get; set; }
        public Texture2D actionBox3 { get; set; }
        public Texture2D actionBox4 { get; set; }
        public Point point { get; set; }
        public string fileName { get; set; }


        public ActionBarComponent(string fileName, Point point)
        {
            this.fileName = fileName;
            this.point = point;
        }
    }
}
