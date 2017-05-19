using GameEngine.Components;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using Game.Components;
using Game.Systems;
using System;

namespace Game
{
    class RPGGame : GameEngine.GameEngine
    {
        protected override void Initialize()
        {
            SystemManager sm = SystemManager.GetInstance();
            sm.AddSystem(new EnemySpawnSystem());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ComponentManager cm = ComponentManager.GetInstance();
            EntityFactory factory = new EntityFactory();

            factory.AddTree(-100, -100);

            for (int i = -640; i <= 640; i += 128)
            {
                cm.AddEntityWithComponents(new IComponent[]
                {
                    new TextureComponent("road1", RenderLayer.Background1),
                    new PositionComponent(i, 150),
                });
            }

            //Enemy spawnsystem
            cm.AddEntityWithComponents(new IComponent[]
            {
                new EnemySpawnComponent(new Point(200, 200), 3, 10000, 200, factory.CreateEnemy(0, 0)),
            });

            cm.AddEntityWithComponents(new IComponent[]
            {
                new TextureComponent("stone1"),
                new PositionComponent(250, 100),
            });

            //Test talk
            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(-200, 0),
                new CollisionComponent(50, 50),
                new InteractComponent(InteractType.Talk),
                new TextComponent( "NewSpriteFont", "Hello darkness my old friend...", new Vector2(Viewport.TitleSafeArea.Width / 2 - 242, Viewport.TitleSafeArea.Bottom -75), Color.Black, false)
            });
            //Test trap
            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(-400, 0),
                new CollisionComponent(50, 50),
                new InteractComponent(InteractType.Trap),
                new AttackComponent(7, 1.0f, 2.2f, WeaponType.Sword),
            });

            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(0, 0),
                new AnimationGroupComponent("PlayerAnimation/MEDIUM/PlayerArmsSpritesheetMEDIUM", new Point(4, 8), 15, RenderLayer.Layer2,
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

            cm.AddEntityWithComponents(factory.CreatePlayer(0, 0));
            
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
               new GUIComponent("UI/Player2-Hp-Ene-Xp", new Point(Viewport.TitleSafeArea.Right, Viewport.TitleSafeArea.Top), RenderLayer.GUI2),
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
               new GUIComponent("UI/ActionBar", new Point(Viewport.TitleSafeArea.Left, Viewport.TitleSafeArea.Bottom - 40), RenderLayer.GUI2),
            });


            //End GUI stuff


            /*cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new TextureComponent("hej"),
                new PositionComponent(40, 0),
            });*/

            //Player two
            //cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[] {
            //    new TextureComponent("hej"),
            //    new HealthComponent(100),
            //    new EnergyComponent(100),
            //    new DamageComponent(),
            //    new PositionComponent(100, 500),
            //    new MoveComponent(0.2f),
            //    new PlayerControlComponent(ControllerType.Gamepad1),
            //    new CollisionComponent(50, 50),
            //    new AttackComponent(100, 0.3f, 0.1f, WeaponType.Sword),
            //    new PlayerComponent(2),
            //    new LevelComponent(2, 55),
            //    new GUIComponent("UI/Player2-Hp-Ene-Xp", gd.Viewport.TitleSafeArea.Right-108, gd.Viewport.TitleSafeArea.Top),
            //    new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
            //});

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new SkillComponent(SkillManager.HeavyAttack, 10, 10, "HeavyAttack")
            });

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new TextureComponent("tree1"),
                new PositionComponent(10, 10),
            });

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new TextureComponent("trees1"),
                new PositionComponent(200, -25),
            });


            //Enemy
            cm.AddEntityWithComponents(factory.CreateEnemy(300, 10));

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
            });
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new ItemComponent(ItemManager.exampleUseItem, "ChainArmorBody", ItemType.Body),
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
            base.LoadContent();
        }
    }
}
