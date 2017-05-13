using System.Collections.Generic;

namespace GameEngine
{
    class DamageComponent : IComponent
    {
        public List<int> IncomingDamageEntityID { get; set; }
        public int LastAttacker { get; set; }

        public DamageComponent()
        {
            IncomingDamageEntityID = new List<int>();
        }
    }
}
