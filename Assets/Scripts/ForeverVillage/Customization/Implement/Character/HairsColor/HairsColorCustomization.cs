using UnityEngine;

namespace Village.Character
{
    public sealed class HairsColorCustomization : MaterialCustomization
    {
        private readonly HairsColorCustomizationConfig _config;

        public HairsColorCustomization(CustomizableCharacter character, HairsColorCustomizationConfig config) : base(config)
        {
            _config = config;

            foreach (Renderer renderer in character.HairRenderers)
                _renderers.Add(renderer);
        }
    }
}

