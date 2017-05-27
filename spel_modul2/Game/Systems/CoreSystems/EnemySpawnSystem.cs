using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
                    if (!cm.HasEntityComponent<HealthComponent>(c.EnemyEntities[i]))
                    {
                        if (c.RespawnCountdown[i] <= 0)
                        {
                            SpawnEnemy(c, i);
                            c.RespawnCountdown[i] = c.RespawnTime;
                        }
                        else
                            c.RespawnCountdown[i] -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
        }

        private void SpawnEnemy(EnemySpawnComponent c, int index)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Random rand = new Random();
            int x, y;
            PositionComponent p = new PositionComponent();
            List<IComponent> template = new List<IComponent>(c.EnemyTemplate);
            int entity;

            for (int i = 0; i < template.Count; i++)
            {
                template[i] = (IComponent)template[i].Clone();
            }

            for (int i = 0; i < MaxSpawnTries; i++)
            {
                x = rand.Next() % c.SpawnMaxRadius;
                y = rand.Next() % c.SpawnMaxRadius;

                if (c.CollisionComponent == null)
                {
                    p.Position = new Vector2(x, y) + c.SpawnLocation.ToVector2();
                    template.Add(p);
                    entity = EntityManager.GetEntityId();
                    cm.AddComponentsToEntity(entity, template.ToArray());
                    c.EnemyEntities[index] = entity;
                    break;
                }
                else
                {
                    Rectangle bb = c.CollisionComponent.CollisionBox;
                    bb.Location = c.SpawnLocation;
                    bb.Location += new Point(x, y);
                    if (CollisionSystem.DetectAreaCollision(bb).Count == 0)
                    {
                        p.Position = new Vector2(x, y) + c.SpawnLocation.ToVector2();
                        template.Add(p);
                        entity = EntityManager.GetEntityId();
                        cm.AddComponentsToEntity(entity, template.ToArray());
                        c.EnemyEntities[index] = entity;
                        break;
                    }
                }
            }
        }
    }
}
