using UnityEngine;

namespace ForeverVillage
{
    public abstract class ArmorConfig : EquipableItemConfig
    {
        [SerializeField] private float _defense;
        [SerializeField] private Armor _prefab;

        public float Defense => _defense;
        public Armor Prefab => _prefab;

        private void OnValidate()
        {
            if (_defense < 0)
                _defense = 0f;
        }
    }
}