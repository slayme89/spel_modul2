﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;
        ComponentManager cm = ComponentManager.GetInstance();
        SystemManager sm = SystemManager.GetInstance();

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            sm.AddSystems(new ISystem[] {
                new AnimationSystem(),
                new AnimationLoaderSystem(),
                new TextureLoaderSystem(),
                new RenderSystem(),
                new MoveSystem(),
                new AISystem()
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            cm.AddComponentsToEntity(1, new IComponent[] {
                new TextureComponent("hej"),
                new PositionComponent(150, 10),
            });

            Debug.WriteLine("APA");

            cm.AddComponentsToEntity(2, new IComponent[]
            {
                new AnimationComponent("PlayerAnimation/NakedFWalk", new Point(4, 1), 150),
                new PositionComponent(10, 10),
                new MoveComponent(),
                new AIComponent(160, 160),
            });

            cm.AddComponentsToEntity(3, new IComponent[]
            {
                new AnimationComponent("threerings", new Point(6, 8), 40),
                new PositionComponent(50, 200),
            });

            sm.GetSystem<AnimationLoaderSystem>().Load(Content);
            sm.GetSystem<TextureLoaderSystem>().Load(Content);
         

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = graphics.GraphicsDevice;
            SpriteBatch sb = new SpriteBatch(gd);
            gd.Clear(Color.Blue);

            sm.GetSystem<RenderSystem>().Render(gd, sb);

            //base.Draw(gameTime);
        }

        KeyboardState previousKeyboardState = new KeyboardState();

        protected override void Update(GameTime gameTime)
        {
            SystemManager.GetInstance().Update<AnimationSystem>(gameTime);
            SystemManager.GetInstance().Update<AISystem>(gameTime);
            SystemManager.GetInstance().Update<MoveSystem>(gameTime);

            var a = cm.GetComponentForEntity<AnimationComponent>(2);
            if (Keyboard.GetState().IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
                a.isPaused = !a.isPaused;

            previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}