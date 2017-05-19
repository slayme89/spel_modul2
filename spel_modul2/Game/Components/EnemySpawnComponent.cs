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
        public int RespawnTimer;
        public IComponent[] EnemyTemplate;
        public int[] EnemyEntities;
        public int MaxEnemies;
        public Point SpawnLocation;
        public int SpawnMaxRadius;
        public IComponent CollisionComponent;

        public EnemySpawnComponent(Point spawnLocation, int maxEnemies, int respawnTimer, int spawnMaxRadius, IComponent[] enemyTemplate)
        {
            SpawnLocation = spawnLocation;
            MaxEnemies = maxEnemies;
            RespawnTimer = respawnTimer;
            SpawnMaxRadius = spawnMaxRadius;
            EnemyTemplate = enemyTemplate;
            RespawnCountdown = new int[maxEnemies];
            EnemyEntities = new int[maxEnemies];

            for (int i = 0; i < enemyTemplate.Length; i++)
            {
                if (enemyTemplate[i] is CollisionComponent)
                {
                    CollisionComponent = enemyTemplate[i];
                    break;
                }
            }
        }
    }
}
