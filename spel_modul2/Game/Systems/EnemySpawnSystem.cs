using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Systems;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Game.Components;

namespace Game.Systems
{
    class EnemySpawnSystem : ISystem
    {
        private int MaxSpawnTries = 5;

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(EnemySpawnComponent c in cm.GetComponentsOfType<EnemySpawnComponent>().Values)
            {
                for (int i = 0; i < c.MaxEnemies; i++)
                {
                    if (c.RespawnTimer <= 0)
                        SpawnEnemy(c, i);
                }
            }
        }

        private void SpawnEnemy(EnemySpawnComponent c, int index)
        {
            for (int i = 0; i < MaxSpawnTries; i++)
            {
                if (c.CollisionComponent == null)
                { }
                else
                {
                    //Rectangle r = c.CollisionComponent.CollisionBox
                }
            }
        }
    }
}
