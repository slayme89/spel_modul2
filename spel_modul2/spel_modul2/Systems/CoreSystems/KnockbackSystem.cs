using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Systems
{
    class KnockbackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<KnockbackComponent>())
            {
                KnockbackComponent knockbackComponent = (KnockbackComponent)entity.Value;
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                if (knockbackComponent.KnockbackActive)
                {
                    moveComponent.Velocity = knockbackComponent.KnockbackDir;
                    knockbackComponent.Cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (cm.HasEntityComponent<SoundComponent>(entity.Key))
                    {
                        cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                    }
                    if(knockbackComponent.Cooldown <= 0.0f)
                    {
                        knockbackComponent.KnockbackActive = false;
                    }
                }
            }
        }
    }
}
