using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class MenuBackgroundComponent : IComponent
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Point Position { get; set; }
        public RenderLayer Layer { get; set; }
        //Fading stuff
        public int mAlphaValue { get; set; }
        public int mFadeIncrement { get; set; }
        public double mFadeDelay { get; set; }

        //Moveing stuff
        public double mFadeDelayMove { get; set; }

        public MenuBackgroundComponent(string name, string texturePath, Point position, RenderLayer layer)
        {
            IsActive = false;
            Name = name;
            TexturePath = texturePath;
            Position = position;
            Layer = Layer;

            //fading stuff
            mAlphaValue = 210;
            mFadeIncrement = 1;
            mFadeDelay = .1;
            mFadeDelayMove = .03;
    }

        public object Clone()
        {
            MenuBackgroundComponent o = (MenuBackgroundComponent)MemberwiseClone();
            o.Name = string.Copy(Name);
            o.TexturePath = string.Copy(TexturePath);
            return o;
        }
    }
}
