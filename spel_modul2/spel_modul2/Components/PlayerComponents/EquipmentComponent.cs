using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class EquipmentComponent : IComponent
    {
        public int Head { get; set; }
        public int Body { get; set; }
        public int Weapon { get; set; }
    }
}
