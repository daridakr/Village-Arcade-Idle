using Arena;
using UnityEngine;

namespace Village
{
    public interface ICharacterSpecialization
    {
        public string Id { get; }
        public SpecializationConfig Data { get; }
        public string GetModelPath();
    }

    public interface ISpecialization
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public SpellsCatalog Spells { get; }
    }
}
