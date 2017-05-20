using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Game.Systems
{
    public class SkillSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                SkillComponent skillComponent = (SkillComponent)entity.Value;
                foreach (int entityUser in skillComponent.UsingEntities)
                {
                    if (cm.HasEntityComponent<EnergyComponent>(entityUser))
                    {
                        EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entityUser);
                        if(skillComponent.EnergyCost < energyComponent.Current)
                        {
                            skillComponent.Use(entityUser);
                            energyComponent.Current -= skillComponent.EnergyCost;
                            skillComponent.CooldownTimer = skillComponent.Cooldown;
                    }
                        //if (skillComponenet.CooldownTimer <= 0 && skillComponenet.EnergyCost < energyComponent.Current)
                        //{
                        //    skillComponenet.Use(entityUser);
                        //    energyComponent.Current -= skillComponenet.EnergyCost;
                        //    skillComponenet.CooldownTimer = skillComponenet.Cooldown;
                        //}
                        //else
                        //{
                        //    skillComponenet.CooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //}
                    }
                }
                skillComponent.UsingEntities.Clear();
            }
        }
    }
}
