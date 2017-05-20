using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;

namespace Game.Systems
{
    public class PlayerAttackSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var Entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                if (cm.HasEntityComponent<AttackComponent>(Entity.Key))
                {
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(Entity.Key);
                    if (attackComponent.Type != WeaponType.None && attackComponent.CanAttack)
                    {
                        PlayerControlComponent playerControl = (PlayerControlComponent)Entity.Value;
                        if (cm.HasEntityComponent<MoveComponent>(Entity.Key))
                        {
                            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(Entity.Key);
                            if (playerControl.Attack.IsButtonDown())
                            {
                                if (attackComponent.AttackCooldown <= 0.0f)
                                {
                                    moveComponent.CanMove = false;
                                    moveComponent.Velocity = new Vector2(0, 0);
                                    cm.GetComponentForEntity<SoundComponent>(Entity.Key).PlayAttackSound = true;
                                    attackComponent.AttackCooldown = attackComponent.RateOfFire;
                                    attackComponent.IsAttacking = true;
                                }
                            }
                            if (attackComponent.AttackCooldown > 0.0f)
                                attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                            else
                                moveComponent.CanMove = true;
                        }
                    }
                }
            }
        }
    }
}
