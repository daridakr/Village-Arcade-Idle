using Arena;
using ForeverVillage;
using UnityEngine;

namespace Village
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] protected string _id;
        [SerializeField] private SpecializationMetadata _metadata;
        [SerializeField] private AvailableWeaponType[] _weaponTypes;
        [SerializeField] private WeaponConfig[] _baseWeapons;
        [SerializeField] private SpellsCatalog _spells;

        public string Id => _id;
        public SpecializationMetadata Meta => _metadata;
        public SpellsCatalog Spells => _spells;
        public IAvailableWeaponType[] WeaponTypes => _weaponTypes;
        public WeaponConfig[] Weapons => _baseWeapons;

        public abstract Specialization InstantiateSpecialization(object condition = null);
    }
}