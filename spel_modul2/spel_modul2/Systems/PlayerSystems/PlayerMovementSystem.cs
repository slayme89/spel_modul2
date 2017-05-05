using System;
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
                if (!cm.HasEntityComponent<MoveComponent>(entity.Key))
                    throw new Exception("MoveComponent not found during player movement system. Entity ID: " + entity.Key);
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                moveComponent.Velocity = ((PlayerControlComponent)entity.Value).Movement.GetDirection();
            }
        }
    }
}
