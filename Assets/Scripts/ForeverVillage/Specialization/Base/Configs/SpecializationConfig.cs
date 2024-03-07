using Arena;
using UnityEngine;

namespace Village
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] private SpecializationMetadata _metadata;
        [SerializeField] private Spell[] _spells;
        //[SerializeField] private Weapon _baseWeapon;

        public SpecializationMetadata Meta => _metadata;
        public Spell[] Spells => _spells;
        //public Weapon Weapon => _baseWeapon;

        public abstract Specialization InstantiateSpecialization(object condition = null);
    }
}