using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class AnimationComponent : IComponent
    {
        public string SpritesheetFilename { get; set; }
        public Texture2D SpriteSheet { get; set; }
        public Point SheetSize { get; set; }
        public Point FrameSize { get; set; }
        public Point CurrentFrame;
        public int FrameDuration { get; set; }
        public int LastFrameDeltaTime { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public bool IsPaused { get; set; }
        public Point Offset;
        public RenderLayer Layer;

        public AnimationComponent(string spritesheetFilename, Point sheetSize, int frameDuration) : this(spritesheetFilename, sheetSize, frameDuration, RenderLayer.Layer1) { }

        public AnimationComponent(string spritesheetFilename, Point sheetSize, int frameDuration, RenderLayer layer)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            SpritesheetFilename = spritesheetFilename;
            SheetSize = sheetSize;
            FrameDuration = frameDuration;
            Layer = layer;

            SpriteSheet = rm.GetResource<Texture2D>(spritesheetFilename);
            FrameSize = new Point(SpriteSheet.Width / SheetSize.X, SpriteSheet.Height / SheetSize.Y);
            Offset = new Point(FrameSize.X / 2, FrameSize.Y / 2);
            SourceRectangle = new Rectangle(new Point(), FrameSize);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}