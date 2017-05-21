using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class ArmorComponent: IComponent
    {
        public int Defence { get; set; }

        public ArmorComponent(int defence)
        {
            Defence = defence;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
