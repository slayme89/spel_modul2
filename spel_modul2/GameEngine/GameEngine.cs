using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class GameEngine : Game
    {
        public GraphicsDeviceManager graphics;
        ComponentManager cm = ComponentManager.GetInstance();
        SystemManager sm = SystemManager.GetInstance();
        GraphicsDevice gd;
        SpriteBatch sb;
        public static GraphicsDevice graphicsDevice;
        RenderHelper renderHelper;
        public Viewport Viewport { get { return graphics.GraphicsDevice.Viewport; } }
        GameStateManager gameStateManager = GameStateManager.GetInstance();

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
            gameStateManager.State = GameState.Menu;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            sm.AddSystems(new object[] {
                new AnimationSystem(),
                new MoveSystem(),
                new CollisionSystem(),
                new RenderSystem(),
                new RenderCollisionBoxSystem(),
                //new InputSystem(),
                new WorldSystem(),
                //new SoundSystem(),
                new RenderGUISystem(),
                new AnimationGroupSystem(),
                new RenderAnimationGroupSystem(),
                new RenderTextSystem(),
                new TextDurationSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sm.GetSystem<WorldSystem>().Load(Content);
            sm.GetSystem<RenderGUISystem>().Load(Content);
            sm.GetSystem<RenderTextSystem>().Load(Content);
            
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.FrontToBack);

            if (gameStateManager.State == GameState.Game)
            {
                gd.Clear(Color.White);
                sm.RenderAllSystems(renderHelper);
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
            if (GameStateManager.GetInstance().State == GameState.Game)
            {
                ComponentManager.GetInstance().Update();
                SystemManager.GetInstance().UpdateAllSystems(gameTime);
            }
            
            if (gameStateManager.State == GameState.Exit)
                Exit();

            base.Update(gameTime);
        }
    }
}