using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using Game.Components;

namespace Game.Systems
{
    class CooldownSystem : ISystem
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

