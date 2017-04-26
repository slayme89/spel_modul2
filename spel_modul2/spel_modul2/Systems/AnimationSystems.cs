using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class AnimationSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<Animation>();

            foreach(Animation animation in animations.Values)
            {
                Update(gameTime, animation);
            }
        }

        private void Update(GameTime gameTime, Animation animation)
        {

        }
    }

    class AnimationLoaderSystem : ISystem
    {
        void ISystem.Update(GameTime gameTime) { }

        public void Load(ContentManager content)
        {
            var animations = ComponentManager.GetInstance().GetComponentsOfType<Animation>();

            if (animations != null)
            {
                foreach (Animation animation in animations.Values)
                {
                    animation.spriteSheet = content.Load<Texture2D>(animation.spritesheetFilename);
                    animation.frameSize = new Point(animation.spriteSheet.Width / animation.sheetSize.X, animation.spriteSheet.Height / animation.sheetSize.Y);
                    animation.sourceRectangle = new Rectangle(new Point(), animation.frameSize);
                }
            }
        }

    }
}