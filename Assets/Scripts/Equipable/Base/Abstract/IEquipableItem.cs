using UnityEngine;

namespace ForeverVillage
{
    public interface IEquipableItem :
        IItem
    {
        // требования, локи
        // кто может экипировать - тип специализации
        public GameObject Prefab { get; }
        public IItemPerk[] Perks { get; }
        public GameObject Equip(Transform rig);
    }
}