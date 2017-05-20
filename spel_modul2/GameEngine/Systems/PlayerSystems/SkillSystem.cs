using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine.Systems
{
    public class SkillSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                CooldownComponent cd = cm.GetComponentForEntity<CooldownComponent>(entity.Key);
                SkillComponent skillComponent = (SkillComponent)entity.Value;
                foreach (int entityUser in skillComponent.UsingEntities)
                {
                    if (cm.HasEntityComponent<EnergyComponent>(entityUser))
                    {
                        EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entityUser);

                        if (skillComponent.EnergyCost < energyComponent.Current && cd.CooldownTimer <= 0)
                        {
                            skillComponent.Use(entityUser);
                            energyComponent.Current -= skillComponent.EnergyCost;
                            cd.CooldownTimer = cd.Cooldown;

                        }
                    }
                }
                skillComponent.UsingEntities.Clear();
            }
        }
    }
}

