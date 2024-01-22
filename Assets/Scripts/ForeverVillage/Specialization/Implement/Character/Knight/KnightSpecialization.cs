namespace ForeverVillage.Scripts.Character
{
    public sealed class KnightSpecialization : CharacterSpecialization
    {
        private readonly KnightSpecializationConfig _config;

        public KnightSpecialization(Gender gender, KnightSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}