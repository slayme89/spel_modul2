using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class AIAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AIComponent ai = cm.GetComponentForEntity<AIComponent>(entity.Key);
                if (ai != null)
                {
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                    if (attackComponent.AttackCooldown <= 0.0f)
                    {
                        moveComponent.canMove = false;
                        attackComponent.AttackCooldown = attackComponent.RateOfFire;
                        attackComponent.IsAttacking = true;
                    }
                    if (attackComponent.AttackCooldown > 0.0f)
                        attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        moveComponent.canMove = true;
                }
            }
        }
    }
}
