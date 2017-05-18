using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
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
}