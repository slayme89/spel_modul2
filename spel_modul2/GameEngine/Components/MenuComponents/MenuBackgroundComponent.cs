using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class MenuBackgroundComponent : IComponent
    {
        public bool IsActive { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public Point Position { get; set; }
        public RenderLayer Layer { get; set; }
        
        //Fading stuff
        public bool HasFadingEffect { get; set; }
        public int mAlphaValue { get; set; }
        public int mFadeIncrement { get; set; }
        public double mFadeDelay { get; set; }

        //Moveing stuff
        public bool HasMovingEffect { get; set; }
        public double mFadeDelayMove { get; set; }

        public MenuBackgroundComponent(string texturePath, Point position, RenderLayer layer, bool fadingEffect, bool movingEffect)
        {
            IsActive = false;
            TexturePath = texturePath;
            Position = position;
            Layer = Layer;
            HasFadingEffect = fadingEffect;
            HasMovingEffect = movingEffect;
            
            //fading stuff
            mAlphaValue = 210;
            mFadeIncrement = 1;
            mFadeDelay = .1;

            //Moving stuff
            mFadeDelayMove = .1;
    }

        public object Clone()
        {
            MenuBackgroundComponent o = (MenuBackgroundComponent)MemberwiseClone();
            o.TexturePath = string.Copy(TexturePath);
            return o;
        }
    }
}
