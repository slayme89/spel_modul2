using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
