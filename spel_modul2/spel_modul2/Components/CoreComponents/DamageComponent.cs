using System.Collections.Generic;

namespace GameEngine
{
    class DamageComponent : IComponent
    {
        public List<int> IncomingDamage { get; set; }
        public int LastAttacker { get; set; }

        public DamageComponent()
        {
            IncomingDamage = new List<int>();
        }
    }
}
