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
            stateManager.State = GameState.Menu;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            sm.AddSystems(new object[] {
                new AnimationSystem(),
                new MoveSystem(),
                new CollisionSystem(),
                new RenderSystem(),
                //new RenderCollisionBoxSystem(),
                new InputSystem(),
                new WorldSystem(),
                new SoundSystem(),
                new RenderGUISystem(),
                new AnimationGroupSystem(),
                new RenderAnimationGroupSystem(),
                new MenuSystem(),
                new RenderMenuSystem(),
                new RenderTextSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sm.GetSystem<WorldSystem>().Load(Content);
            sm.GetSystem<RenderGUISystem>().Load(Content);
            sm.GetSystem<RenderMenuSystem>().Load(Content);
            sm.GetSystem<RenderTextSystem>().Load(Content);
            
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {

            sb.Begin(SpriteSortMode.FrontToBack);
            gd.Clear(Color.Blue);

            //Normal gameplay state
            if (stateManager.State == GameState.Game)
            {
                sm.RenderAllSystems(renderHelper);
            }

            //Menu state
            if (stateManager.State == GameState.Menu)
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
            if (stateManager.State == GameState.Game)
            {
                ComponentManager.GetInstance().Update();
                SystemManager.GetInstance().UpdateAllSystems(gameTime);
            }

            //Menu state
            if (stateManager.State == GameState.Menu)
            {
                SystemManager.GetInstance().Update<InputSystem>(gameTime);
                SystemManager.GetInstance().Update<MenuSystem>(gameTime);
            }

            

            base.Update(gameTime);
        }
    }
}