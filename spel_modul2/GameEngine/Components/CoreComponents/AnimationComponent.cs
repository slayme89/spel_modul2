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

        public AnimationComponent(string spritesheetFilename, Point sheetSize, int frameDuration)
        {
            this.SpritesheetFilename = spritesheetFilename;
            this.SheetSize = sheetSize;
            this.FrameDuration = frameDuration;
            Layer = RenderLayer.Layer1;
        }

        public AnimationComponent(string spritesheetFilename, Point sheetSize, int frameDuration, RenderLayer layer)
        {
            this.SpritesheetFilename = spritesheetFilename;
            this.SheetSize = sheetSize;
            this.FrameDuration = frameDuration;
            this.Layer = layer;
        }
    }
}