using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class AnimationSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<AnimationComponent>();

            foreach(AnimationComponent animation in animations.Values)
            {
                Update(gameTime, animation);
            }
        }

        private void Update(GameTime gameTime, AnimationComponent animation)
        {
            if (animation.isPaused)
                return;

            animation.lastFrameDeltaTime += gameTime.ElapsedGameTime.Milliseconds;
            if(animation.lastFrameDeltaTime > animation.frameDuration)
            {
                //Update current frame
                animation.currentFrame.X = (animation.currentFrame.X + 1) % animation.sheetSize.X;
                if (animation.currentFrame.X == 0)
                    animation.currentFrame.Y = (animation.currentFrame.Y + 1) % animation.sheetSize.Y;

                //Calculate new source rectangle into the spritesheet
                animation.sourceRectangle = new Rectangle(animation.currentFrame * animation.frameSize, animation.frameSize);

                animation.lastFrameDeltaTime -= animation.frameDuration;
            }
        }
    }

    class AnimationLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<AnimationComponent>();

            foreach (AnimationComponent animation in animations.Values)
            {
                animation.spriteSheet = content.Load<Texture2D>(animation.spritesheetFilename);
                animation.frameSize = new Point(animation.spriteSheet.Width / animation.sheetSize.X, animation.spriteSheet.Height / animation.sheetSize.Y);
                animation.offset = new Point(animation.frameSize.X / 2, animation.frameSize.Y / 2);
                animation.sourceRectangle = new Rectangle(new Point(), animation.frameSize);
            }
        }
    }
}