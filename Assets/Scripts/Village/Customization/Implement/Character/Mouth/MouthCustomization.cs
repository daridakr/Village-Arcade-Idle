namespace Village.Character
{
    public sealed class MouthCustomization : MeshCustomization
    {
        private readonly MouthCustomizationConfig _config;

        public MouthCustomization(ICustomizableMouthCharacter character, MouthCustomizationConfig config) : base(config)
        {
            _config = config;

            _saveMaterial = true;
            _meshFilter = character.MouthMesh;
        }
    }
}