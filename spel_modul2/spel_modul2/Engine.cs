using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Engine : Game
    {
        private GraphicsDeviceManager graphics;
        Texture2D h;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            SystemManager.GetInstance().AddSystems(new ISystem[] {
                new AnimationSystem(),
                new AnimationLoaderSystem(),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            h = Content.Load<Texture2D>("hej");
            ComponentManager cm = ComponentManager.GetInstance();

            ComponentManager.GetInstance().AddComponentsToEntity(1, new IComponent[] {
                new Location() { X = 2, Y = 2 },
                new Texture() { texture = h }
            });

            cm.AddComponentsToEntity(2, new IComponent[]
            {
                new AnimationComponent("threerings", new Point(6, 8), 40),
            });

            SystemManager.GetInstance().GetSystem<AnimationLoaderSystem>().Load(Content);

            base.LoadContent();
        }

        Vector2 pos = new Vector2(100, 100);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = graphics.GraphicsDevice;
            SpriteBatch sp = new SpriteBatch(gd);
            gd.Clear(Color.Blue);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                pos.Y -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                pos.Y += 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                pos.X -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                pos.X += 5;
            }

            /*var hej = ComponentManager.GetInstance().GetComponentsForEntity(1);
            var hej2 = ComponentManager.GetInstance().GetComponentsOfType<Location>();*/

            sp.Begin();

            //if (hej.ContainsKey(typeof(Texture)))
            //{
            //Texture b = (Texture)hej[typeof(Texture)];
            Texture b = ComponentManager.GetInstance().GetComponentForEntity<Texture>(1);
            //sp.Draw(b.texture, position: pos);
            sp.Draw(b.texture, pos, Color.Black);
            //}

            /*if (pos.X < 0)
                pos.X = 0;

            if (pos.Y < 0)
                pos.Y = 0;*/

            var a = ComponentManager.GetInstance().GetComponentForEntity<AnimationComponent>(2);

            sp.Draw(a.spriteSheet, position: new Vector2(50, 50), sourceRectangle: a.sourceRectangle);

            sp.End();

            //base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            SystemManager.GetInstance().Update<AnimationSystem>(gameTime);

            var a = ComponentManager.GetInstance().GetComponentForEntity<AnimationComponent>(2);
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                a.isPaused = !a.isPaused;

            base.Update(gameTime);
        }
    }

    /*sealed class Entity
    {
        public int Id;
    }*/

    public class Location : IComponent
    {
        public Vector2 position;
        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
    }

    public class Texture : IComponent
    {
        public Texture2D texture;
    }
}