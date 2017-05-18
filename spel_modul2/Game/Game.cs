using System;
using GameEngine;
using GameEngine.Components;
using Microsoft.Xna.Framework;
using GameEngine.Managers;

namespace Test
{
    class RPGGame : GameEngine.GameEngine
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ComponentManager cm = ComponentManager.GetInstance();

            for (int i = -640; i <= 640; i += 128)
            {
                cm.AddEntityWithComponents(new IComponent[]
                {
                    new TextureComponent("road1", RenderLayer.Background2),
                    new PositionComponent(i, 150),
                });
            }

            cm.AddEntityWithComponents(new IComponent[]
            {
                new TextureComponent("stone1"),
                new PositionComponent(250, 100),
            });

            cm.AddEntityWithComponents(new IComponent[]
            {
                new PositionComponent(-200, 0),
                new CollisionComponent(50, 50),
            });

            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
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
                new PositionComponent(0, 0),
                new MoveComponent(0.2f),
                new PlayerControlComponent(ControllerType.Keyboard),
                new CollisionComponent(50, 50),
                new AttackComponent(10, 0.3f, 0.1f, WeaponType.Sword),
                new PlayerComponent(1),
                new LevelComponent(1, 80),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new ActionBarComponent(),
                new GUIComponent("UI/Player1-Hp-Ene-Xp", Viewport.TitleSafeArea.Left, Viewport.TitleSafeArea.Top),
                new InventoryComponent(5, 4),
                new EnergyComponent(100),
                new DamageComponent(),
                new StatsComponent(5, 1, 0, 0),
                new KnockbackComponent(),
            });

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




            /*cm.AddEntityWithComponents(new IComponent[]
            {
                new AnimationGroupComponent("PlayerSpritesheet", new Point(4, 4), 150,
                new[] {
                    //new Tuple<Point, Point>(new Point(0, 0), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 1), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 2), new Point(4, 1)),
                    new Tuple<Point, Point>(new Point(0, 3), new Point(4, 1)),
                }),
                new PositionComponent(200, 200),
            });*/

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


            //Enemy
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new AnimationComponent("PlayerAnimation/NakedFWalk", new Point(4, 1), 150),
                new HealthComponent(50),
                new PositionComponent(300, 10),
                new MoveComponent(0.1f),
                new AIComponent(160, 160, false),
                new CollisionComponent(50, 50),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
                new LevelComponent(5),
                new DamageComponent(),
                new KnockbackComponent(),
            });
            /*cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new AnimationComponent("threerings", new Point(6, 8), 40),
                new PositionComponent(50, 200),
                new CollisionComponent(50, 50),
                new InteractComponent(),
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
            });*/

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

            ////////////////////////////////////////////////////////////////////
            //////////////// Menu Entities ///////////////////////////////////
            ////////////////////////////////////////////////////////////////////
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
