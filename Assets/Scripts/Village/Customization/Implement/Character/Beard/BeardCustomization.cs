namespace Village.Character
{
    public sealed class BeardCustomization : MeshCustomization
    {
        private readonly BeardCustomizationConfig _config;

        public BeardCustomization(ICustomizableBeardCharacter customizable, BeardCustomizationConfig config) : base(config)
        {
            _config = config;

            _meshFilter = customizable.BeardMesh;
        }
    }
}