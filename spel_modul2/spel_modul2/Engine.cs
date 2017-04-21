using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            Texture2D h = Content.Load<Texture2D>("hej");

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