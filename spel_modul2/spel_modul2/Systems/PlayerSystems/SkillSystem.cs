using Microsoft.Xna.Framework;

namespace GameEngine
{
    class SkillSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var Entity in cm.GetComponentsOfType<SkillComponent>())
            {
                SkillComponent skillComponenet = cm.GetComponentForEntity<SkillComponent>(Entity.Key);
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(Entity.Key);
                EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(Entity.Key);
                if(skillComponenet.SkillType == SkillType.HeavyAttack)
                {
                    if(skillComponenet.CooldownTimer <= 0 && skillComponenet.EnergyCost < energyComponent.Current)
                    {
                        attackComponent.Damage = attackComponent.Damage * 2;
                        energyComponent.Current = skillComponenet.EnergyCost;
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
