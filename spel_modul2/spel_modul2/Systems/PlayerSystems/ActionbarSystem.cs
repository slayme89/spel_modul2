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
    class ActionBarSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {

            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;

            }

        }

        public void Load(ContentManager content)
        {
            var actionBarBox = ComponentManager.GetInstance().GetComponentsOfType<ActionBarComponent>();

            foreach (ActionBarComponent actionbar in actionBarBox.Values)
            {
                actionbar.actionBox1 = content.Load<Texture2D>(actionbar.fileName);
                actionbar.actionBox2 = content.Load<Texture2D>(actionbar.fileName);
                actionbar.actionBox3 = content.Load<Texture2D>(actionbar.fileName);
                actionbar.actionBox4 = content.Load<Texture2D>(actionbar.fileName);
            }
        }
    }
}
