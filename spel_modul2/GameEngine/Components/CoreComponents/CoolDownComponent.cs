using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    
    public class CooldownComponent : IComponent
    {
        public float CooldownTimer { get; set; }
        public int Cooldown { get; set; }

        public CooldownComponent(int cooldown)
        {
            CooldownTimer = 0;
            Cooldown = cooldown;
            
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
