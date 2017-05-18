using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameEngine.Managers;

namespace Game
{
    public class EntityFactory
    {
        public IComponent[] CreateEnemy(int x, int y)
        {
            return new IComponent[]
            {
                new AnimationComponent("PlayerAnimation/NakedFWalk", new Point(4, 1), 150),
                new HealthComponent(50),
                new PositionComponent(x, y),
                new MoveComponent(0.1f),
                new AIComponent(160, 160, false),
                new CollisionComponent(50, 50),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
                new LevelComponent(5),
                new DamageComponent(),
                new KnockbackComponent(),
            };
        }

        public void AddTree(int x, int y)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            cm.AddEntityWithComponents(new TextureComponent("tree1_layer1", RenderLayer.Background2), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer2", RenderLayer.Background3), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer3", RenderLayer.Layer2), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new CollisionComponent(23, 19), new PositionComponent(x + 1, y + 22));
        }

        public IComponent[] CreatePlayer(int x, int y)
        {
            return new IComponent[]
            {
                new AnimationGroupComponent("PlayerSpritesheet", new Point(4, 4), 150,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(1, 1)),
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
                new HealthComponent(100),
                new PositionComponent(x, y),
                new MoveComponent(0.2f),
                new PlayerControlComponent(ControllerType.Keyboard),
                new CollisionComponent(10, 15),
                new AttackComponent(10, 0.3f, 0.1f, WeaponType.Sword),
                new PlayerComponent(1),
                new LevelComponent(1, 80),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new ActionBarComponent(),
                new GUIComponent("UI/Player1-Hp-Ene-Xp", GUIPosition.Left),
                new InventoryComponent(5, 4),
                new EnergyComponent(100),
                new DamageComponent(),
                new StatsComponent(5, 1, 0, 0),
                new KnockbackComponent(),
            };
        }
    }
}
