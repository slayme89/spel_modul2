using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Stick
    {
        private Vector2 stickDirection;

        public Stick()
        {
            stickDirection = new Vector2();
        }

        public void SetDirection(Vector2 newDirection)
        {
            stickDirection = newDirection;
        }

        public Vector2 GetDirection()
        {
            return stickDirection;
        }
    }
}
