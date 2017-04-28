using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameEngine
{
    class MoveSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<MoveComponent>())
            {
                MoveComponent moveComponent = (MoveComponent)entity.Value;
                Vector2 velocity = moveComponent.Velocity;
                PositionComponent position = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                if (position == null)
                    throw new Exception("You must have a position component to be able to move an entity. Entity ID:" + entity.Key);
                float x = velocity.X;
                float y = velocity.Y;
                x *= (float)gameTime.ElapsedGameTime.TotalMilliseconds * moveComponent.Speed;
                y *= (float)gameTime.ElapsedGameTime.TotalMilliseconds * moveComponent.Speed;
                Point futurePosition = position.position + new Point((int)x, (int)y);
                if (!CollisionSystem.DetectMovementCollision(entity.Key, futurePosition))
                {
                    position.position = futurePosition;
                }
                else
                {
                    Debug.WriteLine("Collision");
                }
                    
                //position.position = futurePosition;
            }
        }
    }
}
