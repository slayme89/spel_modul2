using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

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

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        protected override void Initialize()
        {
            gd = graphics.GraphicsDevice;
            sb = new SpriteBatch(gd);

            sm.AddSystems(new ISystem[] {
                new AnimationSystem(),
                new AnimationLoaderSystem(),
                new TextureLoaderSystem(),
                new RenderSystem(),
                new MoveSystem(),
                new PlayerMovementSystem(),
                new AISystem(),
                new InputSystem()
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            cm.AddComponentsToEntity(1, new IComponent[] {
                new TextureComponent("hej"),
                new PositionComponent(150, 10),
                new MoveComponent(1.0f),
                new PlayerControlComponent("Keyboard"),
                new CollisionComponent(50, 50, new Point(150, 10))
            });

            Debug.WriteLine("APA");

            cm.AddComponentsToEntity(2, new IComponent[]
            {
                new AnimationComponent("PlayerAnimation/NakedFWalk", new Point(4, 1), 150),
                new PositionComponent(10, 10),
                new MoveComponent(0.2f),
                new AIComponent(160, 160),
                new CollisionComponent(50, 50, new Point(160, 160))
            });

            cm.AddComponentsToEntity(3, new IComponent[]
            {
                new AnimationComponent("threerings", new Point(6, 8), 40),
                new PositionComponent(50, 200),
                new CollisionComponent(50, 50, new Point(50, 200))
            });

            sm.GetSystem<AnimationLoaderSystem>().Load(Content);
            sm.GetSystem<TextureLoaderSystem>().Load(Content);
         

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            gd.Clear(Color.Blue);

            sm.GetSystem<RenderSystem>().Render(gd, sb);

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            //base.Draw(gameTime);
        }

        KeyboardState previousKeyboardState = new KeyboardState();

        protected override void Update(GameTime gameTime)
        {

            //elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //if (elapsed >= delay)
            //{
            //    if (frames >= 3)
            //        frames = 0;
            //    else
            //        frames++;
            //    elapsed = 0;
            //}

            SystemManager.GetInstance().Update<AnimationSystem>(gameTime);
            SystemManager.GetInstance().Update<AISystem>(gameTime);
            SystemManager.GetInstance().Update<InputSystem>(gameTime);
            SystemManager.GetInstance().Update<PlayerMovementSystem>(gameTime);
            SystemManager.GetInstance().Update<MoveSystem>(gameTime);

            var a = cm.GetComponentForEntity<AnimationComponent>(2);
            if (Keyboard.GetState().IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
                a.isPaused = !a.isPaused;

            previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}