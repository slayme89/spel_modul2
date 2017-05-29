using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;

namespace Game.Systems
{
    public class PlayerMovementSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach(var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControlComponent = (PlayerControlComponent)entity.Value;
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                if (moveComponent.CanMove)
                {
                    moveComponent.Velocity = playerControlComponent.Movement.GetDirection() * moveComponent.Speed;
                    if (playerControlComponent.Movement.GetDirection() != new Vector2(0.0f, 0.0f))
                    {
                        if (cm.HasEntityComponent<AnimationGroupComponent>(entity.Key))
                        {
                            AnimationGroupComponent animGroupComp = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key);
                        } 
                        if (cm.HasEntityComponent<SoundComponent>(entity.Key))
                        {
                            cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Play;
                        }
                    }
                    else
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Pause;
                }
                else
                    cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Walk"].Action = SoundAction.Pause;
            }
        }
    }
}
