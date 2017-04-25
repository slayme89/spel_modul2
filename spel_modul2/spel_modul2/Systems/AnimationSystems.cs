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
                }
            }
        }

    }
}