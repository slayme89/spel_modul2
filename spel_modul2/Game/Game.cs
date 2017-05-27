using GameEngine.Components;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using Game.Components;
using Game.Systems;
using System;
using Game.Managers;
using Game.Systems.CoreSystems;

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
                //new ItemIconLoaderSystem(),
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
                new PlayerMovementSystem(),
                new SkillSystem(),
                new PlayerSpriteTurnSystem(),
                new KnockbackSystem(),
                new CooldownSystem(),
                new GUISystem(),
            });

            base.Initialize();

            //Late Update
            sm.AddSystems(new object[] {
                new PlayerArmSystem(),
                new PlayerEquipmentSystem(),
            });
        }

        protected override void LoadContent()
        {
            SystemManager sm = SystemManager.GetInstance();

            ComponentManager cm = ComponentManager.GetInstance();
            EntityFactory factory = new EntityFactory();

            // Left oob
            for (int i = 0; i <= 5; i++)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
                {
                    new TextureComponent("forest", RenderLayer.Background1),
                    new PositionComponent(-500, -600 + 600 * i)
                });
            }
            // Right oob
            for (int i = 0; i <= 5; i++)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
                {
                    new TextureComponent("forest", RenderLayer.Background1),
                    new PositionComponent(128 * 40 + 500,  300 +600 * i)
                });
            }
            // top oob
            for (int i = 0; i <= 5; i++)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
                {
                    new TextureComponent("forest", RenderLayer.Background2),
                    new PositionComponent(500 + 1000 * i, -300)
                });
            }
            // bot oob
            for (int i = 0; i <= 5; i++)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
                {
                    new TextureComponent("forest", RenderLayer.Background2),
                    new PositionComponent(500 + 1000 * i, 128 * 14 + 300)
                });
            }

            //Water tree
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Background2),
                    new PositionComponent(0, 128*9)
            });

            //Water tree bot
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("stone1", RenderLayer.Foreground1),
                    new PositionComponent(128 * 17 - 4, 128 * 14 - 5)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Foreground2),
                    new PositionComponent(128 * 17 +50, 128 * 13 + 90)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Foreground1),
                    new PositionComponent(128 * 17 + 100, 128 * 13 + 90)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Foreground2),
                    new PositionComponent(128 * 17 + 150, 128 * 13 + 90)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Foreground1),
                    new PositionComponent(128 * 17 + 200, 128 * 13 + 90)
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new TextureComponent("tree1", RenderLayer.Foreground2),
                    new PositionComponent(128 * 17 + 250, 128 * 13 + 90)
            });


            ////////////// Map bounds ///////////////////////////////

            //Left
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(40, 128 * 14),
                    new PositionComponent(0, 128 * 7)
            });
            // Top
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 * 40, 40),
                    new PositionComponent(128 * 20, 0)
            });
            //Right
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(40, 128 * 14),
                    new PositionComponent(128 * 40, 128 * 7)
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
            //water - other
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new CollisionComponent(128 + 164, 128 * 5),
                    new PositionComponent(128 * 18 - 3, 128 * 11 + 54)
            });


            //Test Talk
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                    new InteractComponent(InteractType.Talk),

                    new TextComponent(
                    "NewSpriteFont",
                    "Hej hej hej hej hej hej hej...",
                    new Vector2(Viewport.TitleSafeArea.Center.X - 240,
                    Viewport.TitleSafeArea.Bottom - 75),
                    Color.Black,
                    false),

                    new TextureComponent("GameWorld/signpost"),
                    new PositionComponent(228, 228)
            });

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

            //Player arms!
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
            cm.AddEntityWithComponents(factory.CreatePlayerOne(128, 128));

            //Enemy
            cm.AddEntityWithComponents(factory.CreateEnemy(350, 600));

            //Enemy
            //cm.AddEntityWithComponents(new EnemySpawnComponent(new Point(350, 600), 3, 1000, 100, factory.CreateEnemy(0, 0)));

            ////Enemy
            //cm.AddEntityWithComponents(new EnemySpawnComponent(new Point(750, 700), 5, 100, 100, factory.CreateEnemy(0, 0)));


            ////////////////////////// GUI /////////////////////////////

            // Dialog window
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new GUIComponent(
                GUIType.Misc,
                "UI/DialogWindow",
                new Point(Viewport.TitleSafeArea.Width / 2 - 255, Viewport.TitleSafeArea.Bottom -80),
                RenderLayer.GUI2)
            });
            // P1 hp, ene, xp
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent(
               GUIType.Player1,
               "UI/Player1-Hp-Ene-Xp",
               new Point(Viewport.TitleSafeArea.Left,Viewport.TitleSafeArea.Top),
               RenderLayer.GUI2),
            });
            //P2 hp, ene, xp
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent(
                   GUIType.Player2,
                   "UI/Player2-Hp-Ene-Xp",
                   new Point(Viewport.TitleSafeArea.Right - 108, Viewport.TitleSafeArea.Top),
                   RenderLayer.GUI2),
            });
            // P1 Ationbar
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent(
                   GUIType.Player1,
                   "UI/ActionBar",
                   new Point(Viewport.TitleSafeArea.Left, Viewport.TitleSafeArea.Bottom - 40),
                   RenderLayer.GUI2),
            });
            //////////////////////////// End GUI ///////////////////

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new SkillComponent(SkillManager.HeavyAttack, 3, "HeavyAttack"),
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
                new SwordComponent(1),
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

            //tree
            factory.AddTree(300, 300);

            //Item sword in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(300, 60),
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
             });
            //Item sword in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(300, 120),
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
             });

            //Item head armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(320, 60),
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
             });

            //Knight body armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(340, 60),
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
             });

            //Archer Head armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(360, 60),
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
             });

            //Archer body armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(380, 60),
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
             });

            //Mage Head armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new PositionComponent(400, 60),
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
             });

            //Mage body armor in world
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
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
             });

            //////////////// Menu Entities ///////////////////////////////////

            //////////////// Main Menu ////////////////////////////////////

            // MenuBackground
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuBackgroundComponent(
                    "Menu/forest-menu",
                    new Point(0, 0),
                    RenderLayer.Menubackground,
                    true,
                    true
                    )
            });
            //Main Menu - Play game
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuButtonComponent(
                    MenuButtonType.MainMenuButton,
                    MenuStateManager.MainPlay,
                    "Menu/PlayN",
                    "Menu/PlayH",
                    new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 100),
                    RenderLayer.MenuButton
                    )
            });
            // Main Menu - Options
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuButtonComponent(
                    MenuButtonType.MainMenuButton,
                    MenuStateManager.MainOptions,
                    "Menu/OptionsN",
                    "Menu/OptionsH",
                    new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 200),
                    RenderLayer.MenuButton
                    )
            });
            // Main Menu - Quit game
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new MenuButtonComponent(
                   MenuButtonType.MainMenuButton,
                   MenuStateManager.MainQuit,
                   "Menu/QuitN",
                   "Menu/QuitH",
                   new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 300),
                   RenderLayer.MenuButton
                   )
            });
            // Main Menu Options - 1Player
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new MenuButtonComponent(
                   MenuButtonType.MainOptionsMenuButton,
                   MenuStateManager.OptionsBack,
                   "Menu/1PlayerN",
                   "Menu/1PlayerH",
                   new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 100),
                   RenderLayer.MenuButton
                   )
            });
            // Main Menu Options - 2Players
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new MenuButtonComponent(
                   MenuButtonType.MainOptionsMenuButton,
                   MenuStateManager.OptionsBack,
                   "Menu/2PlayersN",
                   "Menu/2PlayersH",
                   new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 200),
                   RenderLayer.MenuButton
                   )
            });
            // Main Menu Options - Back
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new MenuButtonComponent(
                   MenuButtonType.MainOptionsMenuButton,
                   MenuStateManager.OptionsBack,
                   "Menu/BackN",
                   "Menu/BackH",
                   new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 300),
                   RenderLayer.MenuButton
                   )
            });

            // Pause Menu //

            // Pause Menu - Resume
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuButtonComponent(
                    MenuButtonType.PauseMainMenuButton,
                    MenuStateManager.PauseResume,
                    "Menu/ResumeN",
                    "Menu/ResumeH",
                    new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 100),
                    RenderLayer.MenuButton
                    )
            });
            // Pause Menu - Quit
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuButtonComponent(
                    MenuButtonType.PauseMainMenuButton,
                    MenuStateManager.PauseQuit,
                    "Menu/QuitN",
                    "Menu/QuitH",
                    new Vector2(Viewport.TitleSafeArea.Center.X - 100, Viewport.TitleSafeArea.Top + 200),
                    RenderLayer.MenuButton
                    )
            });
            
            //End of menu entities

            //sm.GetSystem<ItemIconLoaderSystem>().Load(Content);
            sm.GetSystem<InventoryLoaderSystem>().Load(Content);
            sm.GetSystem<SkillLoaderSystem>().Load(Content);
            sm.GetSystem<RenderEnergySystem>().Load(Content);
            sm.GetSystem<RenderHealthSystem>().Load(Content);
            sm.GetSystem<RenderExperienceSystem>().Load(Content);

            base.LoadContent();
        }
    }
}
