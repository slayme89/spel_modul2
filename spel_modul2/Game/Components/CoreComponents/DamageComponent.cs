using GameEngine.Components;
using System;
using System.Collections.Generic;

namespace Game.Components
{
    public class DamageComponent : IComponent
    {
        public List<int> IncomingDamage { get; set; }
        public int LastAttacker { get; set; }
        public int[] DamageReduction { get; set; }

        public DamageComponent()
        {
            DamageReduction = new int[2];
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
