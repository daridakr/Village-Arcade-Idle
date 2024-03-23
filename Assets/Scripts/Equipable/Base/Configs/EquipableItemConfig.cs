using UnityEngine;

namespace ForeverVillage
{
    public abstract class EquipableItemConfig : ItemConfig
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ItemPerk[] _perks;

        public GameObject Prefab => _prefab;
        public ItemPerk[] Perks => _perks;
    }
}