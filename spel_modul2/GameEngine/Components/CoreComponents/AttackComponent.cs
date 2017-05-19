﻿using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public enum WeaponType { Sword, Bow, Magic }

    public class AttackComponent : IComponent
    {
        public int Damage { get; set; }
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
            Damage = damage;
            RateOfFire = rateOfFire;
            Type = weaponType;
            AttackCooldown = 0.0f;
            AttackChargeUp = attackDelay;
            AttackDelay = attackDelay;
            CanAttack = true;
        }
    }
}