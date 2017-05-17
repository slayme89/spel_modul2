using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface ActionBarSlotComponent 
    {
        Action Use { get; set; }
        bool IsItem { get; set; }
    }
}
