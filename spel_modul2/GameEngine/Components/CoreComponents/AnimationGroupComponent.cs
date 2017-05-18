using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class AnimationGroupComponent : IComponent
    {
        public string SpritesheetFilename { get; set; }
        public Texture2D Spritesheet { get; set; }
        public Point SheetSize { get; set; }
        public Tuple<Point, Point>[] Animations { get; set; }
        public Point FrameSize { get; set; }
        public int FrameDuration { get; set; }
        public bool IsPaused { get; set; }
        private int activeAnimation;
        public int ActiveAnimation
        {
            get { return activeAnimation; }
            set
            {
                activeAnimation = value;
                groupFrame = new Point(0, 0);
                currentFrame = groupFrame + Animations[activeAnimation].Item1;
            }
        }
        public int lastFrameDeltaTime { get; set; }
        public Point groupFrame;
        public Point currentFrame;
        public Rectangle sourceRectangle { get; set; }
        public Point offset;
        public RenderLayer layer;

        public AnimationGroupComponent(string spritesheetFilename, Point sheetSize, int frameDuration, params Tuple<Point, Point>[] animations)
        {
            this.SpritesheetFilename = spritesheetFilename;
            this.SheetSize = sheetSize;
            this.Animations = animations;
            this.FrameDuration = frameDuration;
            this.layer = RenderLayer.Layer1;
        }

        public AnimationGroupComponent(string spritesheetFilename, Point sheetSize, int frameDuration, RenderLayer layer, params Tuple<Point, Point>[] animations)
        {
            this.SpritesheetFilename = spritesheetFilename;
            this.SheetSize = sheetSize;
            this.Animations = animations;
            this.FrameDuration = frameDuration;
            this.layer = layer;
        }
    }
}