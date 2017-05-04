using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class WorldComponent : IComponent
    {
        public Dictionary<Point, Texture2D> tiles;
        public Point center;

        public WorldComponent()
        {
            tiles = new Dictionary<Point, Texture2D>();
        }
    }
}