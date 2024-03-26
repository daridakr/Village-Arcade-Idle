using UnityEngine;

namespace Village.Character
{
    public interface ICustomizableCharacterFactory
    {
        public CustomizableCharacter Create(CustomizableCharacter prefab, Transform parent);
    }
}