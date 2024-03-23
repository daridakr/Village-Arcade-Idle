using UnityEngine;

namespace ForeverVillage
{
    public abstract class EquipableItem : Item,
        IEquipableItem
    {
        private readonly EquipableItemConfig _config;

        public IItemPerk[] Perks => _config.Perks;
        public abstract GameObject Prefab { get; }

        protected EquipableItem(EquipableItemConfig config) : base(config) => _config = config;

        public GameObject Equip(Transform rig)
        {
            if (Prefab == null)
                return null;
            
            return Object.Instantiate(Prefab, rig);
        }
    }
}