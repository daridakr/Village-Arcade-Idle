namespace Village.Character
{
    public sealed class EyebrowsCustomization : MeshCustomization
    {
        private readonly EyebrowsCustomizationConfig _config;

        public EyebrowsCustomization(CustomizableCharacter character, EyebrowsCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = character.BrowsMesh;
        }
    }
}