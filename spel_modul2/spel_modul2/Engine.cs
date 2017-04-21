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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            h = Content.Load<Texture2D>("hej");

            ComponentManager.GetComponentManager().AddComponentsToEntity(1, new IComponent[] {
                new Location() { X = 2, Y = 2 },
                new Texture() { texture = h }
            });

            base.LoadContent();
        }

        Vector2 pos = new Vector2(10, 10);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = graphics.GraphicsDevice;
            SpriteBatch sp = new SpriteBatch(gd);
            gd.Clear(new Color(0, 0, 255));

            /*if (Keyboard.GetState().IsKeyDown(Keys.W))
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
            }*/

            var hej = ComponentManager.GetComponentManager().GetComponentsForEntity(1);
            var hej2 = ComponentManager.GetComponentManager().GetComponentsOfType<Location>();

            sp.Begin();

            if (hej.ContainsKey(typeof(Texture)))
            {
                Texture b = (Texture)hej[typeof(Texture)];
                sp.Draw(b.texture, position: pos);
            }

            /*if (pos.X < 0)
                pos.X = 0;

            if (pos.Y < 0)
                pos.Y = 0;*/
            
            sp.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    sealed class Entity
    {
        public int Id;
    }

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

    interface IComponent { }
    interface ISystem
    {
        void Update();
    }
}