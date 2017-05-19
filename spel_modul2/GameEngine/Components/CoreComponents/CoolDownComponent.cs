using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    
    class CooldownComponent : IComponent
    {
        public int Cooldown { get; set; }
        public float CooldownTimer { get; set; }

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
