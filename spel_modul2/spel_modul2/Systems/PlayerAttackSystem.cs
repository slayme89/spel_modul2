using System;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class PlayerAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var Entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControl = cm.GetComponentForEntity<PlayerControlComponent>(Entity.Key);
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(Entity.Key);
                if (attackComponent.AttackCooldown <= 0.0f && playerControl.Attack.IsButtonDown())
                {
                    attackComponent.AttackCooldown = attackComponent.RateOfFire;
                    attackComponent.IsAttacking = true;
                }
                if(attackComponent.AttackCooldown > 0.0f)
                    attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
