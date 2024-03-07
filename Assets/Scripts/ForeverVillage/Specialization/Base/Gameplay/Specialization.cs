using Arena;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Village
{
    public abstract class Specialization : ISpecialization
    {
        [ReadOnly][ShowInInspector] public string Title => _config.Meta.Title;
        [ReadOnly][ShowInInspector] public string Description => _config.Meta.Description;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Meta.Icon;
        public Spell[] Spells => _config.Spells;
        //public IEquippable Weapon => _config.Weapon;

        private readonly SpecializationConfig _config;

        public Specialization(SpecializationConfig config) => _config = config;

        public abstract string GetModelPath();
    }
}