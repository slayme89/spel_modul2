using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    class SkillSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                SkillComponent skillComponenet = cm.GetComponentForEntity<SkillComponent>(entity.Key);
                if (skillComponenet.IsActivated && cm.HasEntityComponent<EnergyComponent>(entity.Key) && cm.HasEntityComponent<AttackComponent>(entity.Key))
                {
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entity.Key);
                    if (skillComponenet.CooldownTimer <= 0 && skillComponenet.EnergyCost < energyComponent.Current)
                    {
                        skillComponenet.Use(entity.Key);
                        energyComponent.Current -= skillComponenet.EnergyCost;
                        skillComponenet.CooldownTimer = skillComponenet.Cooldown;
                    }
                    else
                    {
                        skillComponenet.CooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
        }
    }
}
