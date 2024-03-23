using UnityEngine;

namespace ForeverVillage
{
    public abstract class WeaponConfig : EquipableItemConfig
    {
        [SerializeField] private float _damage;

        public float Damage => _damage;

        private void OnValidate()
        {
            if (_damage < 0)
                _damage = 0f;
        }
    }
}