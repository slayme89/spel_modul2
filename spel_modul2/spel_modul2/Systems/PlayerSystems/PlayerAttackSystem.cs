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
                if (cm.HasEntityComponent<AttackComponent>(Entity.Key))
                {
                    AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(Entity.Key);
                    if (attackComponent.CanAttack)
                    {
                        PlayerControlComponent playerControl = (PlayerControlComponent)Entity.Value;
                        if (cm.HasEntityComponent<MoveComponent>(Entity.Key))
                        {
                            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(Entity.Key);
                            if (playerControl.Attack.IsButtonDown())
                            {
                                if (attackComponent.AttackCooldown <= 0.0f)
                                {
                                    moveComponent.canMove = false;
                                    cm.GetComponentForEntity<SoundComponent>(Entity.Key).PlayAttackSound = true;
                                    attackComponent.AttackCooldown = attackComponent.RateOfFire;
                                    attackComponent.IsAttacking = true;
                                }
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
    }
}
