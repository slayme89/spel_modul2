using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;

        //test skit - some drawing stuff
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;

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

            //test skit - position for the "hej" texture
            position = new Vector2(0, 0);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //test skit - load the "hej" texture
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("hej");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //test skit - controls keyboard
            KeyboardState keyState = Keyboard.GetState();
            //D is pressed
            if (keyState.IsKeyDown(Keys.D))
                position.X += 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //A is pressed
            if (keyState.IsKeyDown(Keys.A))
                position.X -= 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //W is pressed
            if (keyState.IsKeyDown(Keys.W))
                position.Y -= 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //S is pressed
            if (keyState.IsKeyDown(Keys.S))
                position.Y += 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //test skit - controls mouse
            //MouseState ms = Mouse.GetState();

            //position.X = ms.X;
            //position.Y = ms.Y;


            //if  out of bounds on the right, reset position to 0
            if (position.X > GraphicsDevice.Viewport.Width)
                position.X = 0;

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
            spriteBatch.Draw(texture, position);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    
}