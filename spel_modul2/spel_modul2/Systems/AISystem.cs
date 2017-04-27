

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine
{
    class AISystem : ISystem
    {


        public void Update(GameTime gameTime)
        {
            var ai = ComponentManager.GetInstance().GetComponentsOfType<AIComponent>();


            foreach(KeyValuePair<int, IComponent> pair in ai)
            {
               PositionComponent positionComp = ComponentManager.GetInstance().GetComponentForEntity<PositionComponent>(pair.Key);
               if(positionComp != null)
                {

                }
            }

        }
    }
}
