using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class WorldComponent : IComponent
    {
        public Dictionary<Point, Texture2D> Tiles;
        public Point Center;

        public WorldComponent()
        {
            Tiles = new Dictionary<Point, Texture2D>();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}