namespace GameEngine
{
    public enum WeaponType
    {
        Sword,
        Bow,
        Magic
    }
    class AttackComponent : IComponent
    {
        public int Damage { get; set; }
        public float RateOfFire { get; set; }
        public float AttackCooldown { get; set; }
        public WeaponType Type { get; set; }
        public bool IsAttacking { get; set; } = false;

        public AttackComponent(int damage, float rateOfFire,  WeaponType weaponType)
        {
            Damage = damage;
            RateOfFire = rateOfFire;
            Type = weaponType;
            AttackCooldown = 0.0f;
        }

    }
}
