using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace Game.Components
{
    public class EnemySpawnComponent : IComponent
    {
        public int[] RespawnCountdown;
        public int RespawnTime;
        public List<IComponent> EnemyTemplate;
        public int[] EnemyEntities;
        public int MaxEnemies;
        public Point SpawnLocation;
        public int SpawnMaxRadius;
        public CollisionComponent CollisionComponent;

        public EnemySpawnComponent(Point spawnLocation, int maxEnemies, int respawnTime, int spawnMaxRadius, IComponent[] enemyTemplate)
        {
            SpawnLocation = spawnLocation;
            MaxEnemies = maxEnemies;
            RespawnTime = respawnTime;
            SpawnMaxRadius = spawnMaxRadius;
            EnemyTemplate = new List<IComponent>(enemyTemplate);
            RespawnCountdown = new int[maxEnemies];
            EnemyEntities = new int[maxEnemies];

            for (int i = EnemyTemplate.Count - 1; i > 0; i--)
            {
                enemyTemplate[i] = (IComponent)enemyTemplate[i].Clone();
                if (enemyTemplate[i] is CollisionComponent)
                {
                    CollisionComponent = (CollisionComponent)EnemyTemplate[i];
                }
                else if(EnemyTemplate[i] is PositionComponent)
                {
                    EnemyTemplate.RemoveAt(i);
                }
            }

            for (int i = 0; i < maxEnemies; i++)
            {
                EnemyEntities[i] = -1;
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
