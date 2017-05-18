using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Components;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    class AnimationGroupSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(AnimationGroupComponent animationGroupComponent in cm.GetComponentsOfType<AnimationGroupComponent>().Values)
            {
                Update(gameTime, animationGroupComponent);
            }
        }

        private void Update(GameTime gameTime, AnimationGroupComponent animationGroupComponent)
        {
            AnimationGroupComponent a = animationGroupComponent;

            if (a.IsPaused)
                return;

            a.lastFrameDeltaTime += gameTime.ElapsedGameTime.Milliseconds;
            if(a.lastFrameDeltaTime > a.FrameDuration)
            {
                a.groupFrame.X = (a.groupFrame.X + 1) % a.Animations[a.ActiveAnimation].Item2.X;
                if (a.groupFrame.X == 0)
                    a.groupFrame.Y = (a.groupFrame.Y + 1) % a.Animations[a.ActiveAnimation].Item2.Y;
                a.currentFrame = a.groupFrame + a.Animations[a.ActiveAnimation].Item1;

                a.sourceRectangle = new Rectangle(a.currentFrame * a.FrameSize, a.FrameSize);

                a.lastFrameDeltaTime -= a.FrameDuration;
            }
        }
    }

    class RenderAnimationGroupSystem : ISystem, IRenderSystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Render(RenderHelper renderHelper)
        {

        }
    }

    class AnimationGroupLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<AnimationGroupComponent>();

            foreach (AnimationGroupComponent animation in animations.Values)
            {
                animation.Spritesheet = content.Load<Texture2D>(animation.SpritesheetFilename);
                animation.FrameSize = new Point(animation.Spritesheet.Width / animation.SheetSize.X, animation.Spritesheet.Height / animation.SheetSize.Y);
                animation.offset = new Point(animation.FrameSize.X / 2, animation.FrameSize.Y / 2);
                animation.sourceRectangle = new Rectangle(new Point(), animation.FrameSize);
            }
        }
    }
}