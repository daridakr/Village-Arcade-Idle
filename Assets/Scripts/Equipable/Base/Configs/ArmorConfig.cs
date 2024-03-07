using UnityEngine;

namespace ForeverVillage
{
    public abstract class ArmorConfig : EquipableItemConfig
    {
        [SerializeField] private float _defense;

        public float Defense => _defense;

        private void OnValidate()
        {
            if (_defense < 0)
                _defense = 0f;
        }
    }
}