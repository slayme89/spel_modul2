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
                new AnimationSystem(),
                new AnimationLoaderSystem(),
                new TextureLoaderSystem(),
                new MoveSystem(),
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
                new RenderHealthSystem(),
                new InventorySystem(),
                new RenderInventorySystem(),
                new RenderActionbarSystem(),
                new InteractSystem(),
                new RenderGUISystem(),
                new EnergySystem(),
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
                new RenderMenuSystem(),
                new SkillLoaderSystem(),
                new SkillSystem(),
                new PlayerArmSystem(),
                new RenderTextSystem(),
                new PlayerSpriteTurnSystem(),
                new SkillManager(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
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