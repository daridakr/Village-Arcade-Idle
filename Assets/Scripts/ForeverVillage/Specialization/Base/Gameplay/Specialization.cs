using Sirenix.OdinInspector;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public abstract class Specialization : ISpecialization
    {
        [ReadOnly][ShowInInspector] public string Title => _config.Meta.Title;
        [ReadOnly][ShowInInspector] public string Description => _config.Meta.Description;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Meta.Icon;

        private readonly SpecializationConfig _config;

        public Specialization(SpecializationConfig config)
        {
            _config = config;
        }
    }
}