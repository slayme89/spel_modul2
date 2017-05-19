using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class ArmComponent : IComponent
    {
        public int playerID { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
