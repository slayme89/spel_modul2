using System;
using System.Collections.Generic;

namespace GameEngine.Components
{
    public class DamageComponent : IComponent
    {
        public List<int> IncomingDamage { get; set; }
        public int LastAttacker { get; set; }

        public DamageComponent()
        {
            IncomingDamage = new List<int>();
        }

        public object Clone()
        {
            DamageComponent o = (DamageComponent)MemberwiseClone();
            o.IncomingDamage = new List<int>(IncomingDamage);
            return o;
        }
    }
}
