using UnityEngine;

namespace ForeverVillage
{
    public interface IEquipableItem :
        IItem
    {
        // требования, локи
        // кто может экипировать - тип специализации
        public Item Prefab { get; }
        public IItemPerk[] Perks { get; }
        public Item Equip(Transform rig);
    }
}