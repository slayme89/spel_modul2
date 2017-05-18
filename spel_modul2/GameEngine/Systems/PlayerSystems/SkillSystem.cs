using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine.Systems
{
    class SkillSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                SkillComponent skillComponent = (SkillComponent)entity.Value;
                foreach (int entityUser in skillComponent.UsingEntities)
                {
                    Debug.WriteLine("inne med usingentities");
                    if (cm.HasEntityComponent<EnergyComponent>(entityUser))
                    {
                        Debug.WriteLine("inne och har energycomp");
                        EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entityUser);
                        if(skillComponent.EnergyCost < energyComponent.Current)
                        {
                            Debug.WriteLine("Inne och använder skill");
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
