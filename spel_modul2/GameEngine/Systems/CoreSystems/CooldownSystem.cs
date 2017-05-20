using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using System.Diagnostics;

namespace GameEngine.Systems
{
    public class CooldownSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<CooldownComponent>())
            {
                CooldownComponent cd = (CooldownComponent)entity.Value;
                if (cd.CooldownTimer > 0)
                {
                    cd.CooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
