using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace GameEngine
{
    class AttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<AttackComponent>())
            {
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                if(attackComponent.Type == WeaponType.Sword)
                {
                    CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity.Key);
                    if (attackComponent.CanAttack && attackComponent.IsAttacking)
                    {
                        if (attackComponent.AttackChargeUp <= 0.0f)
                        {
                            attackComponent.IsAttacking = false;
                            attackComponent.AttackChargeUp = attackComponent.AttackDelay;
                            collisionComponent.attackCollisionBox = GetAttackRect(entity.Key);
                            collisionComponent.checkAttackColision = true;
                        }
                        else
                        {
                            attackComponent.AttackChargeUp -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                            collisionComponent.checkAttackColision = false;
                        }
                    }
                    else
                    {
                        collisionComponent.checkAttackColision = false;
                    }
                }
                else if(attackComponent.Type == WeaponType.Bow)
                {

                }
                else if (attackComponent.Type == WeaponType.Magic)
                {

                }

            }
        }

        private Rectangle GetAttackRect(int key)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(key);
            CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(key);
            PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(key);
            int range = collisionComponent.collisionBox.Size.X;
            Point hitOffset = new Point((collisionComponent.collisionBox.Width / 2), (collisionComponent.collisionBox.Height / 2));
            return new Rectangle(positionComponent.position - hitOffset + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size);
        }
    }
}
