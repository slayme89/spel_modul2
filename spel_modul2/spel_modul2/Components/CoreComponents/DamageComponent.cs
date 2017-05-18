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
    }
}
