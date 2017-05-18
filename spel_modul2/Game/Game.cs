using System;
using GameEngine;
using GameEngine.Components;
using Microsoft.Xna.Framework;
using GameEngine.Managers;

namespace Game
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

            cm.AddEntityWithComponents(factory.CreatePlayer(0, 0));

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
