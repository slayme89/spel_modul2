using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    class AnimationLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<AnimationComponent>();

            foreach (AnimationComponent animation in animations.Values)
            {
                animation.SpriteSheet = content.Load<Texture2D>(animation.SpritesheetFilename);
                animation.FrameSize = new Point(animation.SpriteSheet.Width / animation.SheetSize.X, animation.SpriteSheet.Height / animation.SheetSize.Y);
                animation.Offset = new Point(animation.FrameSize.X / 2, animation.FrameSize.Y / 2);
                animation.SourceRectangle = new Rectangle(new Point(), animation.FrameSize);
            }
        }
    }
}
