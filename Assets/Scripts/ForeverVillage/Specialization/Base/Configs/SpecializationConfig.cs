using Arena;
using ForeverVillage;
using UnityEngine;

namespace Village
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] private SpecializationMetadata _metadata;
        [SerializeField] private Spell[] _spells;
        [SerializeField] private AvailableWeaponType[] _weaponTypes;
        [SerializeField] private WeaponConfig[] _baseWeapons;

        public SpecializationMetadata Meta => _metadata;
        public Spell[] Spells => _spells;
        public IAvailableWeaponType[] WeaponTypes => _weaponTypes;
        public WeaponConfig[] Weapons => _baseWeapons;

        public abstract Specialization InstantiateSpecialization(object condition = null);
    }
}