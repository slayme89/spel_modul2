using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
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
