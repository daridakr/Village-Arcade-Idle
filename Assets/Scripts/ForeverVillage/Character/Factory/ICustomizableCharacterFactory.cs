using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public interface ICustomizableCharacterFactory
    {
        public CustomizableCharacter Create(CustomizableCharacter prefab, Transform parent);
    }
}