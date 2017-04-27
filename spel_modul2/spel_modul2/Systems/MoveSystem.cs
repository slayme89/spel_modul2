using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class MoveSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<MoveComponent>())
            {
                Vector2 velocity = ((MoveComponent)entity.Value).Velocity;
                PositionComponent position = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                if (position == null)
                    throw new Exception("You must have a position component to be able to move an entity. Entity ID:" + entity.Key);
                float x = velocity.X;
                float y = velocity.Y;
                x *= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                y *= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                position.Position += new Point((int)x, (int)y);
            }
        }
    }
}
