using Arena;
using ForeverVillage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Village
{
    public abstract class Specialization : 
        ISpecialization,
        ISpecializationWeaponTypes
    {
        [ReadOnly][ShowInInspector] public string Title => _config.Meta.Title;
        [ReadOnly][ShowInInspector] public string Description => _config.Meta.Description;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Meta.Icon;
        public SpellsCatalog Spells => _config.Spells;
        public WeaponConfig[] Weapons => _config.Weapons;
        public IAvailableWeaponType[] WeaponTypes => _config.WeaponTypes;

        private readonly SpecializationConfig _config;

        public Specialization(SpecializationConfig config) => _config = config;

        public abstract string GetModelPath();
    }
}