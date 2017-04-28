using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class PlayerMovementSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach(var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                if (moveComponent == null)
                    throw new Exception("MoveComponent not found during player movement system. Entity ID: " + entity.Key);
                PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;
                moveComponent.Velocity = playerControl.Movement.GetDirection();
            }
        }
    }
}
