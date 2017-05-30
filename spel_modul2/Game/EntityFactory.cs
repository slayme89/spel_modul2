using GameEngine.Components;
using Game.Components;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Managers;
using Game.Managers;
using Microsoft.Xna.Framework.Graphics;

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
                new HealthComponent(3, 10.0f),
                new PositionComponent(x, y),
                new MoveComponent(0.5f),
                new AIComponent(160, 160, false),
                new CollisionComponent(25, 35),
                new SoundComponent(new string[]{"Walk", "Attack", "Hurt" }, new string[]{ "Sound/walk", "Sound/sword", "Sound/skeletondamage" }),
                new AttackComponent(1, 0.8f, 0.35f, WeaponType.Sword),
                new LevelComponent(2),
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

        // One tree
        public void AddOneTree(int x, int y)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            cm.AddEntityWithComponents(new TextureComponent("tree1_layer1", RenderLayer.Background2), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer2", RenderLayer.Background3), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer3", RenderLayer.Layer5), new PositionComponent(x, y));
            cm.AddEntityWithComponents(new CollisionComponent(23, 19), new PositionComponent(x + 1, y + 22));
        }
        // Five trees
        public void AddTreeChunk(int x, int y)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            //top trees
            cm.AddEntityWithComponents(new TextureComponent("GameWorld/trees5top", RenderLayer.Layer5), new PositionComponent(x, y));
            //bot tree right
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer1", RenderLayer.Background2), new PositionComponent(x + 36, y + 45));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer2", RenderLayer.Background3), new PositionComponent(x + 36, y + 45));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer3", RenderLayer.Foreground1), new PositionComponent(x + 36, y + 45));
            //bot tree left
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer1", RenderLayer.Background2), new PositionComponent(x - 36, y + 45));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer2", RenderLayer.Background3), new PositionComponent(x - 36, y + 45));
            cm.AddEntityWithComponents(new TextureComponent("tree1_layer3", RenderLayer.Foreground1), new PositionComponent(x - 36, y + 45));
            //top trees collisions
            cm.AddEntityWithComponents(new CollisionComponent(155, 19), new PositionComponent(x, y + 15));
            cm.AddEntityWithComponents(new CollisionComponent(155, 19), new PositionComponent(x, y - 20));
            cm.AddEntityWithComponents(new CollisionComponent(110, 80), new PositionComponent(x, y - 55));
            //bot left collision
            cm.AddEntityWithComponents(new CollisionComponent(23, 30), new PositionComponent(x - 34, y + 47));
            //bot mid collision
            cm.AddEntityWithComponents(new CollisionComponent(23, 30), new PositionComponent(x, y + 32));
            //bot right collision
            cm.AddEntityWithComponents(new CollisionComponent(23, 30), new PositionComponent(x + 35, y + 47));
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
                new HealthComponent(2),
                new PositionComponent(x, y),
                new MoveComponent(0.8f),
                new PlayerControlComponent(ControllerType.Keyboard),
                new CollisionComponent(30, 35),
                new AttackComponent(1, 0.3f, 0.1f, WeaponType.None),
                new PlayerComponent(1),
                new LevelComponent(1, 0),
                new SoundComponent(new string[]{"Walk", "Attack", "Hurt" }, new string[]{ "Sound/walk", "Sound/sword", "Sound/damage" }),
                new ActionBarComponent(),
                new InventoryComponent(5, 4, new Point(10, 100)),
                new EnergyComponent(1),
                new StatsComponent(0, 0, 0, 0),
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
                new HealthComponent(2),
                new PositionComponent(x, y),
                new MoveComponent(0.8f),
                new PlayerControlComponent(ControllerType.Gamepad1),
                new CollisionComponent(30, 35),
                new AttackComponent(1, 0.3f, 0.1f, WeaponType.None),
                new PlayerComponent(2),
                new LevelComponent(1, 0),
                new SoundComponent(new string[]{"Walk", "Attack", "Hurt" }, new string[]{ "Sound/walk", "Sound/sword", "Sound/damage" }),
                new ActionBarComponent(),
                new InventoryComponent(5, 4, new Point(500, 100)),
                new EnergyComponent(1),
                new StatsComponent(0, 0, 0, 0),
                new KnockbackComponent(),
            };
        }


        public IComponent[] CreateKnightHeadArmor(int x, int y)
        {
            return new IComponent[]
            {
                new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "KnightHelmetHead", ItemType.Head),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/KnightArmorHeadSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(5),
            };
        }

        public IComponent[] CreateKnightBodyArmor(int x, int y)
        {
            return new IComponent[]
            {
             new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "KnightArmorBody", ItemType.Body),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/KnightArmorBodySpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(10),
            };
        }

        public IComponent[] CreateArcherHeadArmor(int x, int y)
        {
            return new IComponent[]
            {
                new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "ArcherHatHead", ItemType.Head),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/ArcherArmorHeadSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(3),
            };
        }

        public IComponent[] CreateArcherBodyArmor(int x, int y)
        {
            return new IComponent[]
            {
                 new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "ArcherArmorBody", ItemType.Body),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/ArcherArmorBodySpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(6),
            };
        }

        public IComponent[] CreateMageHeadArmor(int x, int y)
        {
            return new IComponent[]
            {
                new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "MageCowlHead", ItemType.Head),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/MageArmorHeadSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(1),
            };
        }

        public IComponent[] CreateMageBodyArmor(int x, int y)
        {
            return new IComponent[]
            {
                new PositionComponent(420, 60),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "MageArmorBody", ItemType.Body),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/MageArmorBodySpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
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
                new ArmorComponent(3),
            };
        }

        public IComponent[] CreateNormalSword(int x, int y)
        {
            return new IComponent[]
            {
                new PositionComponent(x, y),
                new CollisionComponent(20, 20),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "Sword", ItemType.Weapon),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/SwordSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer4,
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
                new SwordComponent(1),
            };
        }

        public IComponent[] CreateSignPost(int x, int y, string text)
        {
            return new IComponent[]
            {
                new InteractComponent(InteractType.Talk),
                    new TextComponent(
                    "NewSpriteFont",
                    text,
                    Color.Black,
                    TextType.DialogBox),
                    new TextureComponent("GameWorld/signpost"),
                    new PositionComponent(x, y)
            };
        }
    }
}
