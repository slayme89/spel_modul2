﻿using Microsoft.Xna.Framework;

namespace GameEngine
{
    class CollisionComponent : IComponent
    {
        public Rectangle collisionBox { get; set; }

        //public CollisionComponent(int height, int width, Point position)
        //{
        //    collisionBox.Height = height;
        //    collisionBox.Width = width;
        //    collisionBox.Location = position;
        //}
    }
}