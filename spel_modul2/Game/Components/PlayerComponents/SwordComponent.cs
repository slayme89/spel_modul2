using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    public class SwordComponent : IComponent
    {
        public int Damage { get; set; }

        public SwordComponent(int damage)
        {
            Damage = damage;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
