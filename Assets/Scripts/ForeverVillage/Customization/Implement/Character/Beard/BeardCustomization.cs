namespace ForeverVillage.Scripts.Character
{
    public sealed class BeardCustomization : MeshCustomization
    {
        private readonly BeardCustomizationConfig _config;

        public BeardCustomization(CustomizableCharacter character, BeardCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = character.BeardMesh;
        }
    }
}