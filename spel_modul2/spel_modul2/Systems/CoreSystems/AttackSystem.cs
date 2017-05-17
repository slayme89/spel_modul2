using Microsoft.Xna.Framework;
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
                AttackComponent attackComponent = (AttackComponent)entity.Value;
                if(attackComponent.Type == WeaponType.Sword)
                {
                    if (cm.HasEntityComponent<CollisionComponent>(entity.Key))
                    {
                        CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity.Key);
                        if (attackComponent.CanAttack && attackComponent.IsAttacking)
                        {
                            cm.GetComponentForEntity<MoveComponent>(entity.Key).Velocity = new Vector2(0.0f, 0.0f);
                            cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                            if (attackComponent.AttackChargeUp <= 0.0f)
                            {
                                attackComponent.IsAttacking = false;
                                attackComponent.AttackChargeUp = attackComponent.AttackDelay;
                                attackComponent.attackCollisionBox = GetAttackRect(entity.Key);
                                collisionComponent.checkAttackCollision = true;
                                foreach (int entityID in CollisionSystem.DetectAreaCollision(attackComponent.attackCollisionBox))
                                {
                                    if (entityID == entity.Key)
                                        continue;
                                    DamageComponent damageComponent = cm.GetComponentForEntity<DamageComponent>(entityID);
                                    if (damageComponent != null)
                                    {
                                        damageComponent.IncomingDamage.Add(attackComponent.Damage);
                                        damageComponent.LastAttacker = entity.Key;
                                    }
                                }
                            }
                            else
                            {
                                attackComponent.AttackChargeUp -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                                collisionComponent.checkAttackCollision = false;
                            }
                        }
                        else
                        {
                            collisionComponent.checkAttackCollision = false;
                        }
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
            if (cm.HasEntityComponent<MoveComponent>(key))
            {
                MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(key);
                CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(key);
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(key);
                int range = collisionComponent.collisionBox.Size.X;
                Point hitOffset = new Point((collisionComponent.collisionBox.Width / 2), (collisionComponent.collisionBox.Height / 2));
                return new Rectangle(positionComponent.position.ToPoint() - hitOffset + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size);
            }
            else
            {
                throw new Exception("Error in GetAttackRectangle method.");
            }
        }
    }
}
