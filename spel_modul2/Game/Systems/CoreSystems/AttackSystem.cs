using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;

namespace Game.Systems
{
    public class AttackSystem : ISystem
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
                        if (attackComponent.CanAttack && attackComponent.IsAttacking)
                        {
                            cm.GetComponentForEntity<MoveComponent>(entity.Key).Velocity = new Vector2(0.0f, 0.0f);
                            cm.GetComponentForEntity<SoundComponent>(entity.Key).PlayWalkSound = false;
                            if (attackComponent.AttackChargeUp <= 0.0f)
                            {
                                attackComponent.IsAttacking = false;
                                attackComponent.AttackChargeUp = attackComponent.AttackDelay;
                                attackComponent.AttackCollisionBox = GetAttackRect(entity.Key);
                                foreach (int entityID in CollisionSystem.DetectAreaCollision(attackComponent.AttackCollisionBox))
                                {
                                    if (entityID == entity.Key || cm.HasEntityComponent<AIComponent>(entity.Key) && cm.HasEntityComponent<AIComponent>(entityID))
                                        continue;
                                    HealthComponent damageComponent = cm.GetComponentForEntity<HealthComponent>(entityID);
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
                            }
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
                AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(key);
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(key);
                int range = attackComponent.AttackCollisionBox.Size.X;
                Point hitOffset = new Point((attackComponent.AttackCollisionBox.Width / 2), (attackComponent.AttackCollisionBox.Height / 2));
                return new Rectangle(positionComponent.Position.ToPoint() - hitOffset + moveComponent.Direction * new Point(range, range), attackComponent.AttackCollisionBox.Size);
            }
            else
            {
                throw new Exception("Error in GetAttackRectangle method.");
            }
        }
    }
}
