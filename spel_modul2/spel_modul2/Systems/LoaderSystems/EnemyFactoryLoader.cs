using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class EnemyFactoryLoader : ISystem
    {
        public void Update(GameTime gameTime) { }
        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<EnemyFactoryComponent>())
            {
                EnemyFactoryComponent enemyFactoryComponent = (EnemyFactoryComponent)entity.Value;
                AnimationComponent animation = enemyFactoryComponent.MeleeAnimationComponent;
                animation.spriteSheet = content.Load<Texture2D>(animation.spritesheetFilename);
                animation.frameSize = new Point(animation.spriteSheet.Width / animation.sheetSize.X, animation.spriteSheet.Height / animation.sheetSize.Y);
                animation.offset = new Point(animation.frameSize.X / 2, animation.frameSize.Y / 2);
                animation.sourceRectangle = new Rectangle(new Point(), animation.frameSize);

                // set to something
            }
        }
    }
}
