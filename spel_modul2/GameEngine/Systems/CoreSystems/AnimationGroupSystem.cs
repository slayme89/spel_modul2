using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameEngine.Components;
using GameEngine.Managers;
using System;
using System.Diagnostics;

namespace GameEngine.Systems
{
    public class AnimationGroupSystem : ISystem
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
            
            if (a.LastFrameTime <= 0.0f)
            {
                a.GroupFrame.X = (a.GroupFrame.X + 1) % a.Animations[a.ActiveAnimation].Item2.X;
                if (a.GroupFrame.X == 0)
                    a.GroupFrame.Y = (a.GroupFrame.Y + 1) % a.Animations[a.ActiveAnimation].Item2.Y;
                a.CurrentFrame = a.GroupFrame + a.Animations[a.ActiveAnimation].Item1;

                a.SourceRectangle = new Rectangle(a.CurrentFrame * a.FrameSize, a.FrameSize);

                a.LastFrameTime = a.FrameDuration;
            }else
            {
                a.LastFrameTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
}