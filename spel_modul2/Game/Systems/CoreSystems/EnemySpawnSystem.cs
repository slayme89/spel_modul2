using Game.Components;
using Game.Managers;
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

            foreach (EnemySpawnComponent c in cm.GetComponentsOfType<EnemySpawnComponent>().Values)
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
            Random r = new Random();
            int shouldEnemyHaveItems = r.Next(0, 3);




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

                    if (shouldEnemyHaveItems > 1)
                        cm.AddComponentsToEntity(entity, new IComponent[] {
                                new InteractComponent(InteractType.Loot),
                                new ItemComponent(AddHealth, "Bread",
                                ItemType.Consumable),
                        });
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

                        if (shouldEnemyHaveItems > 1)
                            cm.AddComponentsToEntity(entity, new IComponent[] {
                                new InteractComponent(InteractType.Loot),
                                new ItemComponent(AddHealth, "Bread",
                                ItemType.Consumable),
                            });
                        break;
                    }
                }
            }
        }

        //temporär fix
        public void AddHealth(int entity, int position)
        {
            HealthComponent h;
            InventoryComponent i;
            ActionBarComponent a;
            ComponentManager cm = ComponentManager.GetInstance();

            if (cm.TryGetEntityComponents(entity, out h, out i, out a))
            {
                h.Current = MathHelper.Clamp(h.Current + 40, 0, h.Max);

                for (int j = 0; j < a.Slots.Length; j++)
                {
                    if (a.Slots[j] == cm.GetComponentForEntity<ItemComponent>(i.Items[position]))
                        a.Slots[j] = null;
                }

                //remove from inventory
                i.ItemsToRemove.Add(i.Items[position]);
                //cm.RemoveEntity(i.Items[position]);
                //i.Items[position] = 0;
            }
        }
    }
}
