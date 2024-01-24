namespace ForeverVillage.Scripts.Character
{
    public sealed class HairCustomization : MeshCustomization
    {
        private readonly HairCustomizationConfig _config;

        public HairCustomization(CustomizableCharacter character, HairCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = character.HairMesh;
        }
    }
}