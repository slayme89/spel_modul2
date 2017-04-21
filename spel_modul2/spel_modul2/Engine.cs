using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    interface IComponent { }
    interface ISystem
    {
        void Update();
    }
}