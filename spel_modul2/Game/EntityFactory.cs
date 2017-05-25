using GameEngine.Components;
using Game.Components;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Managers;
using System.Diagnostics;

namespace Game
{
    public class EntityFactory
    {
        public IComponent[] CreateEnemy(int x, int y)
        {
            return new IComponent[]
            {
                new AnimationGroupComponent("SkeletonSpritesheet", new Point(4, 9), 150, 
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 4), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 5), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 6), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 7), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 8), new Point(4, 1)),
                }),
                new HealthComponent(50, 10.0f),
                new PositionComponent(x, y),
                new MoveComponent(0.5f),
                new AIComponent(160, 160, false),
                new CollisionComponent(25, 35),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/skeletondamage"),
                new AttackComponent(10, 0.8f, 0.35f, WeaponType.Sword),
                new LevelComponent(5),
                new KnockbackComponent(),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(AddHealth, "Bread", ItemType.Consumable),
            };
        }

        private void AddHealth(int entity, int position)
        {
            HealthComponent h;
            InventoryComponent i;
            ActionBarComponent a;
            ComponentManager cm = ComponentManager.GetInstance();

            if (cm.TryGetEntityComponents(entity, out h, out i, out a))
            {
                h.Current = MathHelper.Clamp(h.Current + 40, 0, h.Max);
                
                for(int j = 0; j < a.Slots.Length; j++)
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

        public void AddTree(int x, int y)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            cm.AddEntityWithComponents(new TextureComponent("tree1_layer1", RenderLayer.Background2), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer2", RenderLayer.Background3), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer3", RenderLayer.Layer5), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new CollisionComponent(23, 19), new PositionComponent(x + 1, y + 22));
        }

        public IComponent[] CreatePlayerOne(int x, int y)
        {
            return new IComponent[]
            {
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/PlayerSpritesheetMEDIUM", new Point(4, 4), 150,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
                new HealthComponent(100),
                new PositionComponent(x, y),
                new MoveComponent(1),
                new PlayerControlComponent(ControllerType.Keyboard),
                new CollisionComponent(30, 35),
                new AttackComponent(10, 0.3f, 0.1f, WeaponType.None),
                new PlayerComponent(1),
                new LevelComponent(1, 80),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new ActionBarComponent(),
                new InventoryComponent(5, 4, new Point(10, 100)),
                new EnergyComponent(100),
                new StatsComponent(5, 1, 0, 0),
                new KnockbackComponent(),
            };
        }

        public IComponent[] CreatePlayerTwo(int x, int y)
        {
            return new IComponent[]
            {
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/PlayerSpritesheetMEDIUM", new Point(4, 4), 150,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
                new HealthComponent(100),
                new PositionComponent(x, y),
                new MoveComponent(0.2f),
                new PlayerControlComponent(ControllerType.Gamepad1),
                new CollisionComponent(30, 35),
                new AttackComponent(10, 0.3f, 0.1f, WeaponType.None),
                new PlayerComponent(2),
                new LevelComponent(1, 80),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new ActionBarComponent(),
                new InventoryComponent(5, 4, new Point(500, 100)),
                new EnergyComponent(100),
                new StatsComponent(5, 1, 0, 0),
                new KnockbackComponent(),
            };
        }
    }
}
