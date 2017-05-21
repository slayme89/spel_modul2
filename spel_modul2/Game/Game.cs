using GameEngine.Components;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using Game.Components;
using Game.Systems;
using System;
using Game.Managers;

namespace Game
{
    class RPGGame : GameEngine.GameEngine
    {
        protected override void Initialize()
        {
            SystemManager sm = SystemManager.GetInstance();
            sm.AddSystems(new object[] {
                new RenderHealthSystem(),
                new RenderInventorySystem(),
                new RenderActionbarSystem(),
                new RenderExperienceSystem(),
                new RenderEnergySystem(),
                new RenderAttackingCollisionBoxSystem(),
                new ItemIconLoaderSystem(),
                new InventoryLoaderSystem(),
                new SkillLoaderSystem(),
                new SkillManager(),
                new AIMovementSystem(),
                new AIAttackSystem(),
                new EnemySpawnSystem(),
                new AttackSystem(),
                new EnergySystem(),
                new HealthSystem(),
                new InteractSystem(),
                new LevelSystem(),
                new ActionBarSystem(),
                new StatsSystem(),
                new InventorySystem(),
                new PlayerAttackSystem(),
                new PlayerArmSystem(),
                new PlayerMovementSystem(),
                new SkillSystem(),
                new PlayerSpriteTurnSystem(),
                new PlayerEquipmentSystem(),
                new DamageSystem(),
                new KnockbackSystem(),
                new CooldownSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SystemManager sm = SystemManager.GetInstance();

            ComponentManager cm = ComponentManager.GetInstance();
            EntityFactory factory = new EntityFactory();


            // Add trees around the map
            for (int i = 0; i <= 10; i++)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
                {
                    new TextureComponent("tree1", RenderLayer.Foreground1),
                    new PositionComponent(-1, i * 105)

                });
            }



            ////////////// Map bounds ///////////////////////////////

            //Left
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(40, 128 * 14),
                    new PositionComponent(0, 128 * 14 / 2)
            });
            // Top
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 * 40, 40),
                    new PositionComponent(128 * 40 / 2, 0)
            });
            //Right
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(40, 128 * 14),
                    new PositionComponent(128 * 40, 128 * 14 / 2)
            });
            //Bot
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 * 40, 40),
                    new PositionComponent(128 * 40 / 2, 128 * 14)
            });
            //Water bridge - left
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 * 6 + 10, 20),
                    new PositionComponent(128 * 3 + 20, 128 * 9 - 12)
            });
            //Water bridge - right
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 * 12 + 30, 20),
                    new PositionComponent(128 * 13, 128 * 9 - 10)
            });
            //water - below bridge
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 + 32, 128 * 5),
                    new PositionComponent(128 * 18 + 63, 128 * 11 + 54)
            });
            



            //factory.AddTree(0, 50);



            //Enemy spawnsystem
            //cm.AddEntityWithComponents(new IComponent[]
            //{
            //    new EnemySpawnComponent(new Point(200, 200), 3, 10000, 200, factory.CreateEnemy(0, 0)),
            //});

            //cm.AddEntityWithComponents(new IComponent[]
            //{
            //    new TextureComponent("stone1"),
            //    new PositionComponent(250, 100),
            //});

            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(0, 0),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/PlayerArmsSpritesheetMEDIUM", new Point(4, 8), 150, RenderLayer.Layer3,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 4), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 5), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 6), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 7), new Point(4, 1)),
                }),
                new ArmComponent(),
            });

            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(100, 100),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/PlayerArmsSpritesheetMEDIUM", new Point(4, 8), 150, RenderLayer.Layer3,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 4), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 5), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 6), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 7), new Point(4, 1)),
                }),
                new ArmComponent(),
            });


            //cm.AddEntityWithComponents(factory.CreatePlayerTwo(100, 100));


            //Player1
            cm.AddEntityWithComponents(factory.CreatePlayerOne(128 * 18, 800));

            //Enemy
            cm.AddEntityWithComponents(factory.CreateEnemy(250, 200));

            //////////////////////////GUI Stuff/////////////////////////////
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new GUIComponent("UI/DialogWindow", new Point(Viewport.TitleSafeArea.Width / 2 - 255, Viewport.TitleSafeArea.Bottom -80), RenderLayer.GUI2)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent("UI/Player1-Hp-Ene-Xp", new Point(Viewport.TitleSafeArea.Left, Viewport.TitleSafeArea.Top), RenderLayer.GUI2),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent("UI/Player2-Hp-Ene-Xp", new Point(Viewport.TitleSafeArea.Right - 108, Viewport.TitleSafeArea.Top), RenderLayer.GUI2),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent("UI/ActionBar", new Point(Viewport.TitleSafeArea.Left, Viewport.TitleSafeArea.Bottom - 40), RenderLayer.GUI2),
            });
            //End GUI stuff

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new SkillComponent(SkillManager.HeavyAttack, 10, "HeavyAttack"),
                new CooldownComponent(3),
            });

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new WorldComponent(),
                new SoundThemeComponent("Sound/theme"),
            });

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "Staff", ItemType.Weapon),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "Sword", ItemType.Weapon),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/SwordSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer3,
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
                new SwordComponent(10),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "GoldArmorHead", ItemType.Head),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "GoldArmorBody", ItemType.Body),

            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "ChainArmorHead", ItemType.Head),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/KnightArmorHeadSpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "ChainArmorBody", ItemType.Body),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/KnightArmorBodySpritesheetMEDIUM", new Point(4, 4), 150, RenderLayer.Layer2,
                new[] {
                    new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
            });

            //Item sword in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(300, 60),
                new CollisionComponent(50, 50),
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
                new SwordComponent(10),
             });

            //Item head armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(300, 60),
                new CollisionComponent(50, 50),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "ChainArmorHead", ItemType.Head),
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
             });

            //Item body armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(300, 60),
                new CollisionComponent(50, 50),
                new InteractComponent(InteractType.Loot),
                new ItemComponent(ItemManager.exampleUseItem, "ChainArmorBody", ItemType.Body),
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
             });

            //////////////// Menu Entities ///////////////////////////////////

            //Background
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuBackgroundComponent("MainMenuBackground", "Menu/MenuBackgroundBlack", new Point(0, 0), RenderLayer.Menubackground)

            });
            //Play in main menu
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuButtonComponent("MainPlay", MenuManager.Play, "Menu/PlayNormal", "Menu/PlayHighlight", new Vector2(100, 100), RenderLayer.MenuButton)
            });
            //Quit in main menu
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new MenuButtonComponent("MainQuit", MenuManager.Quit, "Menu/QuitNormal", "Menu/QuitHighlight", new Vector2(100, 140), RenderLayer.MenuButton),
            });
            //End of menu entities

            sm.GetSystem<ItemIconLoaderSystem>().Load(Content);
            sm.GetSystem<InventoryLoaderSystem>().Load(Content);
            sm.GetSystem<SkillLoaderSystem>().Load(Content);
            sm.GetSystem<RenderEnergySystem>().Load(Content);
            sm.GetSystem<RenderHealthSystem>().Load(Content);
            sm.GetSystem<RenderExperienceSystem>().Load(Content);

            base.LoadContent();
        }
    }
}
