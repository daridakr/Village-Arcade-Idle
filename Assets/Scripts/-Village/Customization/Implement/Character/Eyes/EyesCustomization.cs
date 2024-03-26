namespace Village.Character
{
    public sealed class EyesCustomization : MeshCustomization
    {
        private readonly EyesCustomizationConfig _config;

        public EyesCustomization(ICustomizableEyesCharacter character, EyesCustomizationConfig config) : base(config)
        {
            _config = config;

            _saveMaterial = true;
            _meshFilter = character.EyesMesh;
        }
    }
}