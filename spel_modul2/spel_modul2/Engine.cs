using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;
        ComponentManager cm = ComponentManager.GetInstance();
        SystemManager sm = SystemManager.GetInstance();
        //float elapsed;
        //float delay = 200f;
        //int frames = 0;
        GraphicsDevice gd;
        SpriteBatch sb;
        public static GraphicsDevice graphicsDevice;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            IsMouseVisible = true;
            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        protected override void Initialize()
        {
            gd = graphics.GraphicsDevice;
            graphicsDevice = graphics.GraphicsDevice;
            sb = new SpriteBatch(gd);

            sm.AddSystems(new ISystem[] {
                new AnimationSystem(),
                new AnimationLoaderSystem(),
                new TextureLoaderSystem(),
                new CollisionSystem(),
                new RenderSystem(),
                new RenderCollisionBoxSystem(),
                new MoveSystem(),
                new PlayerMovementSystem(),
                new AIMovementSystem(),
                new InputSystem(),
                new AttackSystem(),
                new PlayerAttackSystem(),
                new RenderAttackingCollisionBoxSystem(gd),
                new WorldSystem(),
                new AIAttackSystem(),
                new SoundSystem(),
                new SoundLoaderSystem(),
                new DamageSystem(),
                new RenderHealthSystem(),
                new LevelSystem(),
                new InventorySystem(),
                new RenderInventorySystem(gd),
                new InteractSystem(),
                new RenderGUISystem(),
                new RenderEnergySystem(),
                new ItemIconLoaderSystem(),
                new HealthSystem(),
                new RenderExperienceSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            for (int i = -640; i <= 640; i += 128)
            {
                cm.AddComponentsToEntity(i, new IComponent[]
                {
                    new TextureComponent("road1"),
                    new PositionComponent(i, 150),
                });
            }

            cm.AddComponentsToEntity(1, new IComponent[]
            {
                new TextureComponent("hej"),
                new HealthComponent(100),
                new PositionComponent(0, 0),
                new MoveComponent(0.2f),
                new PlayerControlComponent(ControllerType.Keyboard),
                new CollisionComponent(50, 50),
                new AttackComponent(50, 0.3f, 0.1f, WeaponType.Sword),
                new PlayerComponent(1),
                new LevelComponent(2, 44),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new GUIComponent("UI/Player1-Hp-Ene-Xp", gd.Viewport.TitleSafeArea.Left, gd.Viewport.TitleSafeArea.Top),
                new InventoryComponent(5, 4),
                new EnergyComponent(100),
                new DamageComponent(),
                new StatsComponent(5, 1, 0, 0),
            });

            cm.AddComponentsToEntity(7, new IComponent[]
            {
                new TextureComponent("tree1"),
                new PositionComponent(10, 10),
            });

            cm.AddComponentsToEntity(8, new IComponent[]
            {
                new TextureComponent("trees1"),
                new PositionComponent(200, -25),
            });

            /*cm.AddComponentsToEntity(60, new IComponent[]
            {
                new TextureComponent("hej"),
                new PositionComponent(40, 0),
            });*/

            /*cm.AddComponentsToEntity(5, new IComponent[] {
                new TextureComponent("hej"),
                new HealthComponent(100),
                new PositionComponent(100, 500),
                new MoveComponent(0.2f),
                new PlayerControlComponent(ControllerType.Gamepad1),
                new CollisionComponent(50, 50),
                new AttackComponent(100, 0.3f, 0.1f, WeaponType.Sword),
                new PlayerComponent(2),
                new LevelComponent(2),
                new GUIComponent("UI/Player2-Hp-Ene-Xp", gd.Viewport.TitleSafeArea.Right, gd.Viewport.TitleSafeArea.Top),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
            });*/

            cm.AddComponentsToEntity(2, new IComponent[]
            {
                new AnimationComponent("PlayerAnimation/NakedFWalk", new Point(4, 1), 150),
                new HealthComponent(50),
                new PositionComponent(300, 10),
                new MoveComponent(0.1f),
                new AIComponent(160, 160, false),
                new CollisionComponent(50, 50),
                new SoundComponent("Sound/walk", "Sound/sword", "Sound/damage"),
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
                new LevelComponent(3),
                new DamageComponent(),
            });

            /*cm.AddComponentsToEntity(3, new IComponent[]
            {
                new AnimationComponent("threerings", new Point(6, 8), 40),
                new PositionComponent(50, 200),
                new CollisionComponent(50, 50),
                new InteractComponent(),
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
            });*/

            cm.AddComponentsToEntity(4, new IComponent[]
            {
                new WorldComponent(),
                new SoundThemeComponent("Sound/theme"),
            });

            cm.AddComponentsToEntity(10, new IComponent[]
            {
                new ItemComponent("Staff", ItemType.Weapon),
            });
            cm.AddComponentsToEntity(11, new IComponent[]
            {
                new ItemComponent("Sword", ItemType.Weapon),
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

            
            
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            gd.Clear(Color.Blue);
            sm.GetSystem<RenderSystem>().Render(gd, sb);
            sm.GetSystem<RenderEnergySystem>().Render(gd, sb);
            sm.GetSystem<RenderHealthSystem>().Render(gd, sb);
            sm.GetSystem<RenderExperienceSystem>().Render(gd, sb);
            sm.GetSystem<RenderCollisionBoxSystem>().Render(gd, sb);
            sm.GetSystem<RenderAttackingCollisionBoxSystem>().Render(gd, sb);
            sm.GetSystem<RenderInventorySystem>().Render(gd, sb);
            sm.GetSystem<RenderGUISystem>().Render(sb);

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            //base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            SystemManager.GetInstance().UpdateAllSystems(gameTime);
            base.Update(gameTime);
        }
    }
}