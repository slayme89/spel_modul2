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
                PlayerControlComponent playerControlComponent = (PlayerControlComponent)entity.Value;
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                moveComponent.Velocity = playerControlComponent.Movement.GetDirection();
                if(playerControlComponent.Movement.GetDirection() != new Vector2(0.0f, 0.0f))
                {
                    if (cm.HasEntityComponent<SoundComponent>(entity.Key))
                    {
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = true;
                    }
                }
            }
        }
    }
}
