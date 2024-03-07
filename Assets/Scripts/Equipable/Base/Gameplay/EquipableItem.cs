namespace ForeverVillage
{
    public abstract class EquipableItem : Item,
        IEquipableItem
    {
        private EquipableItemConfig _config;
        private bool _isEquipped;

        public IItemPerk[] Perks => _config.Perks;

        protected EquipableItem(EquipableItemConfig config) : base(config)
        {
            _config = config;
        }

        public void Equip()
        {
            if (_isEquipped)
                return;

            _isEquipped = true;
        }

        public void Unequip()
        {
        }
    }
}