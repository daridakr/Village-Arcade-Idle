namespace ForeverVillage
{
    public abstract class Weapon : EquipableItem,
        IWeapon
    {
        private readonly WeaponConfig _config;

        public float Damage => _config.Damage;

        public abstract WeaponType Type { get; }
        public abstract WeaponBodyPart BodyPart { get; }

        public Weapon(WeaponConfig config) : base(config)
        {
            _config = config;
        }
    }
}