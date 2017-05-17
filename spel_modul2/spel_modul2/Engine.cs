using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Engine : Game
    {
        private GraphicsDeviceManager graphics;
        ComponentManager cm = ComponentManager.GetInstance();
        SystemManager sm = SystemManager.GetInstance();
        GraphicsDevice gd;
        SpriteBatch sb;
        public static GraphicsDevice graphicsDevice;
        private RenderHelper renderHelper;

        //GameState Manager
        StateManager stateManager = StateManager.GetInstance();


        // Frame rate related stuff
        private float frameCount = 0.0f;
        private float elapsedTime = 0.0f;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            IsMouseVisible = true;

            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            gd = graphics.GraphicsDevice;
            sb = new SpriteBatch(gd);
            renderHelper = new RenderHelper(gd, sb);
            stateManager.SetState("Game");

            sm.AddSystems(new object[] {
                new AnimationSystem(),
                new AnimationLoaderSystem(),
                new TextureLoaderSystem(),
                new CollisionSystem(),
                new RenderSystem(),
                new RenderCollisionBoxSystem(),
                new PlayerMovementSystem(),
                new AIMovementSystem(),
                new InputSystem(),
                new AttackSystem(),
                new PlayerAttackSystem(),
                new RenderAttackingCollisionBoxSystem(),
                new WorldSystem(),
                new AIAttackSystem(),
                new SoundSystem(),
                new SoundLoaderSystem(),
                new DamageSystem(),
                new KnockbackSystem(),
                new MoveSystem(),
                new RenderHealthSystem(),
                new InventorySystem(),
                new RenderInventorySystem(),
                new RenderActionbarSystem(),
                new InteractSystem(),
                new RenderGUISystem(),
                new RenderEnergySystem(),
                new ItemIconLoaderSystem(),
                new HealthSystem(),
                new RenderExperienceSystem(),
                new AnimationGroupSystem(),
                new AnimationGroupLoaderSystem(),
                new RenderAnimationGroupSystem(),
                new InventoryLoaderSystem(),
                new ActionBarSystem(),
                new LevelSystem(),
                new StatsSystem(),
                new MenuSystem(),
                new RenderMenuSystem()
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            


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
                new GUIComponent("UI/Player1-Hp-Ene-Xp", gd.Viewport.TitleSafeArea.Left, gd.Viewport.TitleSafeArea.Top),
                new InventoryComponent(5, 4),
                new EnergyComponent(100),
                new DamageComponent(),
                new StatsComponent(5, 1, 0, 0),
                new KnockbackComponent(),
                new SkillComponent(SkillManager.HeavyAttack, 10, 10)
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

            //Menu
            cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                new MenuBackgroundComponent("MainMenuBackground", "Menu/MenuBackground", new Vector2(0, 0), RenderLayer.Menubackground),
                new MenuButtonComponent("MainPlay", MenuManager.Play, "Menu/PlayNormal", "Menu/PlayHighlight", new Vector2(100, 100), RenderLayer.MenuButton),
                //new MenuButtonComponent("MainOptions", null, "Menu/OptionsNormal", "Menu/OptionsHighlight", new Vector2(100, 140), RenderLayer.MenuButton),
                //new MenuButtonComponent("MainQuit", null, "Menu/PlayNormal", "Menu/QuitHighlight", new Vector2(100, 180), RenderLayer.MenuButton),
            });

            sm.GetSystem<AnimationLoaderSystem>().Load(Content);
            sm.GetSystem<TextureLoaderSystem>().Load(Content);
            sm.GetSystem<WorldSystem>().Load(Content);
            sm.GetSystem<SoundLoaderSystem>().Load(Content);
            sm.GetSystem<RenderEnergySystem>().Load(Content);
            sm.GetSystem<RenderHealthSystem>().Load(Content);
            sm.GetSystem<RenderExperienceSystem>().Load(Content);
            sm.GetSystem<RenderGUISystem>().Load(Content);
            sm.GetSystem<ItemIconLoaderSystem>().Load(Content);
            sm.GetSystem<InventoryLoaderSystem>().Load(Content);
            sm.GetSystem<RenderMenuSystem>().Load(Content);


            sm.GetSystem<AnimationGroupLoaderSystem>().Load(Content);
            
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {

            sb.Begin(SpriteSortMode.FrontToBack);
            gd.Clear(Color.Blue);

            //Normal gameplay state
            if (stateManager.GetState() == "Game")
            {
                sm.RenderAllSystems(renderHelper); 
            }

            //Menu state
            if(stateManager.GetState() == "Menu")
            {
                //Only render the menu (RenderMenuSystem)
                sm.Render<RenderMenuSystem>(renderHelper);
            }

            sb.End();


            frameCount++;
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= 1.0f)
            {
                elapsedTime -= 1.0f;
                Window.Title = frameCount.ToString();
                frameCount = 0.0f;
            }
            

            //base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            //Normal gameplay state
            if (stateManager.GetState() == "Game")
            {
                ComponentManager.GetInstance().Update();
                SystemManager.GetInstance().UpdateAllSystems(gameTime);
            }

            //Menu state
            if (stateManager.GetState() == "Menu")
            {
                SystemManager.GetInstance().Update<InputSystem>(gameTime);
                SystemManager.GetInstance().Update<MenuSystem>(gameTime);
            }

            

            base.Update(gameTime);
        }
    }
}