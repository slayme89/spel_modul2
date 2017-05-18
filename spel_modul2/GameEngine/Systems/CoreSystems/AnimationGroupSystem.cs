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

            if (a.isPaused)
                return;

            a.lastFrameDeltaTime += gameTime.ElapsedGameTime.Milliseconds;
            if(a.lastFrameDeltaTime > a.frameDuration)
            {
                a.groupFrame.X = (a.groupFrame.X + 1) % a.animations[a.activeAnimation].Item2.X;
                if (a.groupFrame.X == 0)
                    a.groupFrame.Y = (a.groupFrame.Y + 1) % a.animations[a.activeAnimation].Item2.Y;
                a.currentFrame = a.groupFrame + a.animations[a.activeAnimation].Item1;

                a.sourceRectangle = new Rectangle(a.currentFrame * a.frameSize, a.frameSize);

                a.lastFrameDeltaTime -= a.frameDuration;
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
                animation.spritesheet = content.Load<Texture2D>(animation.spritesheetFilename);
                animation.frameSize = new Point(animation.spritesheet.Width / animation.sheetSize.X, animation.spritesheet.Height / animation.sheetSize.Y);
                animation.offset = new Point(animation.frameSize.X / 2, animation.frameSize.Y / 2);
                animation.sourceRectangle = new Rectangle(new Point(), animation.frameSize);
            }
        }
    }
}