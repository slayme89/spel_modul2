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
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                if (cm.HasEntityComponent<AttackComponent>(entity.Key))
                {
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                    if (attackComponent.Type != WeaponType.None)
                    {
                        PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;
                        if (cm.HasEntityComponent<MoveComponent>(entity.Key))
                        {
                            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                            if (attackComponent.CanAttack)
                            {
                                if (playerControl.Attack.IsButtonDown())
                                {
                                    if (attackComponent.AttackCooldown <= 0.0f)
                                    {
                                        moveComponent.CanMove = false;
                                        moveComponent.Velocity = new Vector2(0, 0);
                                        cm.GetComponentForEntity<SoundComponent>(entity.Key).Sounds["Attack"].Action = SoundAction.Play;
                                        attackComponent.AttackCooldown = attackComponent.RateOfFire;
                                        attackComponent.IsAttacking = true;
                                    }
                                }
                                if (attackComponent.AttackCooldown <= 0.0f)
                                    moveComponent.CanMove = true;
                            }
                            if (attackComponent.AttackCooldown > 0.0f)
                                attackComponent.AttackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
            }
        }
    }
}
