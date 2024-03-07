using UnityEngine;

namespace ForeverVillage
{
    public abstract class EquipableItem : Item,
        IEquipableItem
    {
        private EquipableItemConfig _config;

        public IItemPerk[] Perks => _config.Perks;
        public abstract Item Prefab { get; }

        protected EquipableItem(EquipableItemConfig config) : base(config) => _config = config;

        public Item Equip(Transform rig)
        {
            if (Prefab == null)
                return null;

            return Instantiate(Prefab, rig);
        }
    }
}