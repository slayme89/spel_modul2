using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;

        //test skit - some drawing stuff
        private SpriteBatch spriteBatch;
        private Texture2D textureforward;
        private Rectangle sourceRect;
        private Rectangle destRect;
        private Vector2 position;
        float elapsed;
        float delay = 200f;
        int frames = 0;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //for debugging
            //removing the fixed timestep and desyncing
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 33);
        }

        protected override void Initialize()
        {
            base.Initialize();

            //test skit - position for the texture
            position = new Vector2(0, 0);
            destRect = new Rectangle(100, 100, 32, 48);

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //test skit 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureforward = Content.Load<Texture2D>("Player/wizwalk");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(elapsed >= delay)
            {
                if (frames >= 3)
                    frames = 0;
                else
                    frames++;
                elapsed = 0;
            }

            sourceRect = new Rectangle(32*frames, 0, 32, 48);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            //for debugging showing the fps in title
            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            //test skit - draw the damn thing
            spriteBatch.Begin();
            spriteBatch.Draw(textureforward, destRect, sourceRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    
}