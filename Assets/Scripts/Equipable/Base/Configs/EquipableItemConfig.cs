using UnityEngine;

namespace ForeverVillage
{
    public abstract class EquipableItemConfig : ItemConfig
    {
        [SerializeField] private ItemPerk[] _perks;

        public ItemPerk[] Perks => _perks;
    }
}