using Sirenix.OdinInspector;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public abstract class Specialization : ISpecialization
    {
        [ReadOnly][ShowInInspector] public string Title => _config.Title;
        [ReadOnly][ShowInInspector] public string Description => _config.Description;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Icon;

        private readonly SpecializationConfig _config;

        public Specialization(SpecializationConfig config)
        {
            _config = config;
        }
    }
}