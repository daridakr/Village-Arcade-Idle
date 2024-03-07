namespace ForeverVillage
{
    public abstract class Weapon : EquipableItem,
        IWeapon
    {
        private WeaponConfig _config;

        public float Damage => _config.Damage;
        public override Item Prefab => _config.Prefab;

        public Weapon(WeaponConfig config) : base(config)
        {
            _config = config;
        }
    }
}