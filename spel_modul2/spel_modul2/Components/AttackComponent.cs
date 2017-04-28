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
        public WeaponType Type { get; set; }

        public AttackComponent(int damage, float rateOfFire,  WeaponType weaponType)
        {
            Damage = damage;
            RateOfFire = rateOfFire;
            Type = weaponType;
        }

    }
}
