using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class SkillComponent : IComponent
    {
        public float CooldownTimer { get; set; }
        public int Cooldown { get; set; }
        public int EnergyCost { get; set; }
        public Texture2D SkillIcon { get; set; }
        public bool IsActivated { get; set; }
        public Action Use { get; set; }
        public SkillComponent(Action action, int cooldown, int energyCost)
        {
            IsActivated = false;
            Cooldown = cooldown;
            CooldownTimer = 0;
            EnergyCost = energyCost;
            Use = action;
        }
    }
}
