using System;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            
        }

        public void entityAttack(int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
        }
    }
}
