using UnityEngine;
using Zenject;

namespace Village.Character
{
    public sealed class CustomizableCharacterFactory : ICustomizableCharacterFactory
    {
        private DiContainer _diContainer;

        public CustomizableCharacterFactory(DiContainer diContainer) => _diContainer = diContainer;

        public CustomizableCharacter Create(CustomizableCharacter prefab, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<CustomizableCharacter>(prefab, parent);
        }
    }
}