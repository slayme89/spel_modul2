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
    class SkillLoaderSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            var skills = cm.GetComponentsOfType<SkillComponent>();
            foreach (var player in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                InventoryComponent invenComp = cm.GetComponentForEntity<InventoryComponent>(player.Key);
                int i = 0;
                foreach (var skill in skills)
                {
                    SkillComponent skillComp = (SkillComponent)skill.Value;
                    skillComp.SkillIcon = content.Load<Texture2D>("SkillIcons/" + skillComp.IconFileName);
                    invenComp.NotPickedSkills[i] = skill.Key;
                    i++;
                }
            }
        }
    }
}
