namespace Village.Character
{
    public sealed class HairCustomization : MeshCustomization
    {
        private readonly HairCustomizationConfig _config;

        public HairCustomization(ICustomizableHairCharacter character, HairCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = character.HairMesh;
        }
    }
}