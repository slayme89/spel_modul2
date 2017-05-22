using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Managers;

namespace GameEngine.Components
{
    public class AnimationGroupComponent : IComponent
    {
        public string SpritesheetFilename { get; set; }
        public Texture2D Spritesheet { get; set; }
        public Point Offset;
        public RenderLayer Layer;
        public Tuple<Point, Point>[] Animations { get; set; }
        public int FrameDuration { get; set; }
        public bool IsPaused { get; set; }
        public int ActiveAnimation
        {
            get { return activeAnimation; }
            set
            {
                activeAnimation = value;
                GroupFrame = new Point(0, 0);
                CurrentFrame = GroupFrame + Animations[activeAnimation].Item1;
            }
        }

        internal Point SheetSize { get; set; }
        internal Point FrameSize { get; set; }
        internal double LastFrameTime { get; set; }
        internal Point GroupFrame;
        internal Point CurrentFrame;
        internal Rectangle SourceRectangle { get; set; }

        private int activeAnimation;        

        public AnimationGroupComponent(string spritesheetFilename, Point sheetSize, int frameDuration, params Tuple<Point, Point>[] animations) : this(spritesheetFilename, sheetSize, frameDuration, RenderLayer.Layer1, animations) { }

        public AnimationGroupComponent(string spritesheetFilename, Point sheetSize, int frameDuration, RenderLayer layer, params Tuple<Point, Point>[] animations)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            SpritesheetFilename = spritesheetFilename;
            SheetSize = sheetSize;
            Animations = animations;
            FrameDuration = frameDuration;
            Layer = layer;

            Spritesheet = rm.GetResource<Texture2D>(spritesheetFilename);
            FrameSize = new Point(Spritesheet.Width / SheetSize.X, Spritesheet.Height / SheetSize.Y);
            Offset = new Point(FrameSize.X / 2, FrameSize.Y / 2);
            SourceRectangle = new Rectangle(new Point(), FrameSize);
        }

        public object Clone()
        {
            AnimationGroupComponent o = (AnimationGroupComponent)MemberwiseClone();
            o.SpritesheetFilename = string.Copy(SpritesheetFilename);

            for (int i = 0; i < Animations.Length; i++)
            {
                o.Animations[i] = new Tuple<Point, Point>(Animations[i].Item1, Animations[i].Item2);
            }

            return o;
        }
    }
}