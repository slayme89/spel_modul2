using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public enum SkillType
    {
        HeavyAttack,
        QuickAttack,
        FireBall
    }

    class SkillComponent : IComponent
    {
        public float CooldownTimer { get; set; }
        public float Cooldown { get; set; }
        public SkillType SkillType { get; set; }
        public int EnergyCost { get; set; }

        public SkillComponent(int cooldown, SkillType skillType, int energyCost)
        {
            Cooldown = cooldown;
            CooldownTimer = 0;
            SkillType = skillType;
            EnergyCost = energyCost;
        }
    }
}
