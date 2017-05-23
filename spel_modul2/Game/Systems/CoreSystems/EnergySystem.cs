using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Systems;
using Game.Components;

namespace Game.Systems
{
    class EnergySystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<EnergyComponent>())
            {
                EnergyComponent es = (EnergyComponent)entity.Value;

                if (es.Current < es.Max)
                {
                    es.Current += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
