using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    public class SkillLoaderSystem : ISystem
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
