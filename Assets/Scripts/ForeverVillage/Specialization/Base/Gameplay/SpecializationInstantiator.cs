using UnityEngine;
using Village.Character;

namespace Village
{
    public sealed class SpecializationInstantiator
    {
        private ICustomizableCharacterFactory _factory;
        private CharacterLoader _loader;

        public SpecializationInstantiator(ICustomizableCharacterFactory factory)
        {
            _factory = factory;

            _loader = new CharacterLoader();
        }

        public CustomizableCharacter Instantiate(Specialization specialization, Transform point)
        {
            CustomizableCharacter customizationPrefab = _loader.LoadCustomizable(specialization.GetPrefabPath());

            return _factory.Create(customizationPrefab, point);
        }
    }
}