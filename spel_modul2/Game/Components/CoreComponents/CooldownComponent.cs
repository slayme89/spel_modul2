using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class CooldownComponent : IComponent
    {
        public float CooldownTimer { get; set; }
        public int Cooldown { get; set; }

        public CooldownComponent(int cooldown)
        {
            Cooldown = cooldown;
            CooldownTimer = 0;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
