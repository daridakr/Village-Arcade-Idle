namespace ForeverVillage
{
    public abstract class Armor : EquipableItem,
        IArmor
    {
        private ArmorConfig _config;

        public float Defense => _config.Defense;
        public override Item Prefab => _config.Prefab;

        public Armor(ArmorConfig config) : base(config)
        {
            _config = config;
        }
    }
}