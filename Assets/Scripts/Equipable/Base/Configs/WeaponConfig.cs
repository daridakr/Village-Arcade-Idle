using UnityEngine;

namespace ForeverVillage
{
    public abstract class WeaponConfig : EquipableItemConfig
    {
        [SerializeField] private float _damage;
        [SerializeField] private Weapon _prefab;

        public float Damage => _damage;
        public Weapon Prefab => _prefab;

        private void OnValidate()
        {
            if (_damage < 0)
                _damage = 0f;
        }
    }
}