using System;
using Microsoft.Xna.Framework;
using GameEngine.Components;

namespace Game.Components
{
    public enum WeaponType { Sword, Bow, Magic, None }

    public class AttackComponent : IComponent
    {
        public int Damage { get; set; }
        public int BonusDamage { get; set; }
        public float RateOfFire { get; set; }
        public float AttackCooldown { get; set; }
        public float AttackChargeUp { get; set; }
        public WeaponType Type { get; set; }
        public bool IsAttacking { get; set; } = false;
        public float AttackDelay { get; set; }
        public bool CanAttack { get; set; }
        public Rectangle AttackCollisionBox { get; set; }

        public AttackComponent(int damage, float rateOfFire, float attackDelay, WeaponType weaponType)
        {
            BonusDamage = 0;
            Damage = damage;
            RateOfFire = rateOfFire;
            Type = weaponType;
            AttackCooldown = 0.0f;
            AttackChargeUp = attackDelay;
            AttackDelay = attackDelay;
            CanAttack = true;
            AttackCollisionBox = new Rectangle(0, 0, 25, 25);
        }

        public AttackComponent(int damage, float rateOfFire, float attackDelay, WeaponType weaponType, int attackSizeX, int attackSizeY)
        {
            BonusDamage = 0;
            Damage = damage;
            RateOfFire = rateOfFire;
            Type = weaponType;
            AttackCooldown = 0.0f;
            AttackChargeUp = attackDelay;
            AttackDelay = attackDelay;
            CanAttack = true;
            AttackCollisionBox = new Rectangle(0, 0, attackSizeX, attackSizeY);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
