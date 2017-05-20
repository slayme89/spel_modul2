using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    public class AnimationSystem : ISystem
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
            if (animation.IsPaused)
                return;

            animation.LastFrameTime += gameTime.ElapsedGameTime.Milliseconds;
            if(animation.LastFrameTime > animation.FrameDuration)
            {
                //Update current frame
                animation.CurrentFrame.X = (animation.CurrentFrame.X + 1) % animation.SheetSize.X;
                if (animation.CurrentFrame.X == 0)
                    animation.CurrentFrame.Y = (animation.CurrentFrame.Y + 1) % animation.SheetSize.Y;

                //Calculate new source rectangle into the spritesheet
                animation.SourceRectangle = new Rectangle(animation.CurrentFrame * animation.FrameSize, animation.FrameSize);

                animation.LastFrameTime -= animation.FrameDuration;
            }
        }
    }
}