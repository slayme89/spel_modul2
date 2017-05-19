using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class GameEngine : Game
    {
        private GraphicsDeviceManager graphics;
        ComponentManager cm = ComponentManager.GetInstance();
        SystemManager sm = SystemManager.GetInstance();
        GraphicsDevice gd;
        SpriteBatch sb;
        public static GraphicsDevice graphicsDevice;
        private RenderHelper renderHelper;
        public Viewport Viewport { get { return graphics.GraphicsDevice.Viewport; } }

        //GameState Manager
        StateManager stateManager = StateManager.GetInstance();


        // Frame rate related stuff
        private float frameCount = 0.0f;
        private float elapsedTime = 0.0f;

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ResourceManager.GetInstance().Content = Content;

            IsFixedTimeStep = false;
            IsMouseVisible = true;

            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            gd = graphics.GraphicsDevice;
            sb = new SpriteBatch(gd);
            renderHelper = new RenderHelper(gd, sb);
            stateManager.SetState("Menu");
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            sm.AddSystems(new object[] {
                new AIAttackSystem(),
                new AIMovementSystem(),
                new ActionBarSystem(),
                new AnimationGroupLoaderSystem(),
                new AnimationGroupSystem(),
                new AnimationSystem(),
                new AttackSystem(),
                new CollisionSystem(),
                new DamageSystem(),
                new EnergySystem(),
                new HealthSystem(),
                new InputSystem(),
                new InteractSystem(),
                new InventoryLoaderSystem(),
                new InventorySystem(),
                new ItemIconLoaderSystem(),
                new KnockbackSystem(),
                new LevelSystem(),
                new MenuSystem(),
                new MoveSystem(),
                new PlayerArmSystem(),
                new PlayerAttackSystem(),
                new PlayerMovementSystem(),
                new PlayerSpriteTurnSystem(),
                new RenderActionbarSystem(),
                new RenderAnimationGroupSystem(),
                new RenderAttackingCollisionBoxSystem(),
                new RenderCollisionBoxSystem(),
                new RenderEnergySystem(),
                new RenderExperienceSystem(),
                new RenderGUISystem(),
                new RenderHealthSystem(),
                new RenderInventorySystem(),
                new RenderMenuSystem(),
                new RenderSystem(),
                new RenderTextSystem(),
                new SkillLoaderSystem(),
                new SkillManager(),
                new SkillSystem(),
                new SoundSystem(),
                new StatsSystem(),
                new WorldSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sm.GetSystem<WorldSystem>().Load(Content);
            sm.GetSystem<RenderEnergySystem>().Load(Content);
            sm.GetSystem<RenderHealthSystem>().Load(Content);
            sm.GetSystem<RenderExperienceSystem>().Load(Content);
            sm.GetSystem<RenderGUISystem>().Load(Content);
            sm.GetSystem<ItemIconLoaderSystem>().Load(Content);
            sm.GetSystem<InventoryLoaderSystem>().Load(Content);
            sm.GetSystem<RenderMenuSystem>().Load(Content);
            sm.GetSystem<SkillLoaderSystem>().Load(Content);
            sm.GetSystem<RenderTextSystem>().Load(Content);
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
            if (stateManager.GetState() == "Menu")
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
            

            base.Draw(gameTime);
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