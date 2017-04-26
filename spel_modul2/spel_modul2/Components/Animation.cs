using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class Animation : IComponent
    {
        public string spritesheetFilename { get; set; }
        public Texture2D spriteSheet { get; set; }
        public Point sheetSize { get; set; }
        public Point frameSize { get; set; }
        public Point currentFrame;
        public int frameDuration { get; set; }
        public int lastFrameDeltaTime { get; set; }
        public Rectangle sourceRectangle { get; set; }

        public Animation(string spritesheetFilename, Point sheetSize, int frameDuration)
        {
            this.spritesheetFilename = spritesheetFilename;
            this.sheetSize = sheetSize;
            this.frameDuration = frameDuration;
        }
    }
}