namespace Village.Character
{
    public sealed class HeadCustomization : MeshCustomization
    {
        private readonly HeadCustomizationConfig _config;

        public HeadCustomization(ICustomizableFaceCharacter character, HeadCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = character.HeadMesh;
        }
    }
}