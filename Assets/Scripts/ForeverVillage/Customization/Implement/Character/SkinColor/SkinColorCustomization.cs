using Unity.VisualScripting;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class SkinColorCustomization : MaterialCustomization
    {
        private readonly SkinColorCustomizationConfig _config;
        private readonly CustomizableCharacter _character;

        public SkinColorCustomization(CustomizableCharacter character, SkinColorCustomizationConfig config) : base(config)
        {
            Debug.Log("Hi");
            _config = config;

            foreach (Renderer renderer in character.Body)
                _renderers.Add(renderer);
        }
    }
}