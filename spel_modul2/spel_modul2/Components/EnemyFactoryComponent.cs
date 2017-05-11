using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    enum EnemyType { MeleeEnemy, RangedEnemy };
    class EnemyFactoryComponent : IComponent
    {
        public AnimationComponent MeleeAnimationComponent { get; set; }
        public SoundComponent MeleeSoundComponent { get; set; }
        //public AnimationComponent RangedAnimationComponent { get; set; }
        //public SoundComponent RangedSoundComponent { get; set; }
        public EnemyFactoryComponent(AnimationComponent meleeAnimationComponent, SoundComponent meleeSoundComponent, AnimationComponent rangedAnimationComponent, SoundComponent rangedSoundComponent)
        {
            MeleeAnimationComponent = meleeAnimationComponent;
            MeleeSoundComponent = meleeSoundComponent;
            //RangedAnimationComponent = rangedAnimationComponent;
            //RangedSoundComponent = rangedSoundComponent;
        }

        public EnemyFactoryComponent(AnimationComponent meleeAnimationComponent, SoundComponent meleeSoundComponent)
        {
            MeleeAnimationComponent = meleeAnimationComponent;
            MeleeSoundComponent = meleeSoundComponent;
        }

        public void AddEnemy(EnemyType type, Point position, int health, float speed, int damage, float rateOfFire, float attackDelay, WeaponType weaponType, int level)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            if(type == EnemyType.MeleeEnemy)
            {
                cm.AddComponentsToEntity(EntityManager.GetEntityId(), new IComponent[]
            {
                MeleeAnimationComponent,
                new HealthComponent(50),
                new PositionComponent(position.X, position.Y),
                new MoveComponent(0.1f),
                new AIComponent(160, 160, false),
                new CollisionComponent(50, 50),
                MeleeSoundComponent,
                new AttackComponent(10, 0.5f, 0.3f, WeaponType.Sword),
                new LevelComponent(3),
                new DamageComponent(),
                new KnockbackComponent(2),
            });
            }
            
        }
    }
}
