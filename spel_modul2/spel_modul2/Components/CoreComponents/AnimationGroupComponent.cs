using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class AnimationGroupComponent : IComponent
    {
        public string spritesheetFilename { get; set; }
        public Texture2D spritesheet { get; set; }
        public Point sheetSize { get; set; }
        public Tuple<Point, Point>[] animations { get; set; }
        public Point frameSize { get; set; }
        public int frameDuration { get; set; }
        public bool isPaused { get; set; }
        private int _activeAnimation;
        public int activeAnimation
        {
            get { return _activeAnimation; }
            set {
                _activeAnimation = value;
                groupFrame = new Point(0, 0);
                currentFrame = groupFrame + animations[_activeAnimation].Item1;
            }
        }
        public int lastFrameDeltaTime { get; set; }
        public Point groupFrame;
        public Point currentFrame;
        public Rectangle sourceRectangle { get; set; }
        public Point offset;

        public AnimationGroupComponent(string spritesheetFilename, Point sheetSize, int frameDuration, params Tuple<Point, Point>[] animations)
        {
            this.spritesheetFilename = spritesheetFilename;
            this.sheetSize = sheetSize;
            this.animations = animations;
            this.frameDuration = frameDuration;
        }
    }
}