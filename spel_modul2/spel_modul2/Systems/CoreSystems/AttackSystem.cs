using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
                if (attackComponent.CanAttack && attackComponent.IsAttacking)
                {
                    if(attackComponent.AttackChargeUp <= 0.0f)
                    {
                        attackComponent.IsAttacking = false;
                        attackComponent.AttackChargeUp = attackComponent.AttackDelay;
                        EntityAttack(gameTime, entity.Key);
                    }
                    else
                        attackComponent.AttackChargeUp -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void EntityAttack(GameTime gameTime, int entity)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            AttackComponent attackComponent = cm.GetComponentForEntity<AttackComponent>(entity);
            MoveComponent moveComponent = cm.GetComponentForEntity<MoveComponent>(entity);
            PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity);
            if(attackComponent.Type == WeaponType.Sword)
            {
                CollisionComponent collisionComponent = cm.GetComponentForEntity<CollisionComponent>(entity);
                int range = collisionComponent.collisionBox.Size.X;
                Point hitOffset = new Point((collisionComponent.collisionBox.Width / 2), (collisionComponent.collisionBox.Height / 2));
                Rectangle hitArea = new Rectangle(positionComponent.position - hitOffset + moveComponent.Direction * new Point(range, range), collisionComponent.collisionBox.Size);

                List<int> entitiesHit = CollisionSystem.DetectAreaCollision(hitArea);
                foreach (int entityHit in entitiesHit)
                {
                    if(entityHit != entity)
                    {
                        HealthComponent entityHitHealth = cm.GetComponentForEntity<HealthComponent>(entityHit);
                        if (entityHitHealth != null && entityHitHealth.IsAlive == true)
                        {
                            //Update health on entity
                            DamageSystem dmgSys = new DamageSystem();
                            dmgSys.Update(entityHit, entity);

                            //Play damage sound
                            cm.GetComponentForEntity<SoundComponent>(entity).PlayDamageSound = true;
                           
                            //Update Level on entity
                            if(cm.HasEntityComponent<LevelComponent>(entity) || cm.HasEntityComponent<LevelComponent>(entityHit))
                            {
                                LevelSystem lvlSys = new LevelSystem();
                                lvlSys.Update(entity, entityHit);
                            }
                        }
                    }
                }
            }else if (attackComponent.Type == WeaponType.Bow)
            {

            }else if (attackComponent.Type == WeaponType.Magic)
            {

            }
        }
    }
}
