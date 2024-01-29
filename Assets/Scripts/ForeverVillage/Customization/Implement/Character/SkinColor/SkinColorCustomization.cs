using UnityEngine;

namespace Village.Character
{
    public sealed class SkinColorCustomization : MaterialCustomization
    {
        private readonly SkinColorCustomizationConfig _config;

        public SkinColorCustomization(CustomizableCharacter character, SkinColorCustomizationConfig config) : base(config)
        {
            _config = config;

            foreach (Renderer renderer in character.SkinRenderers)
                _renderers.Add(renderer);
        }
    }
}