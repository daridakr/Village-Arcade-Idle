namespace Village.Character
{
    public class ArcherSpecialization : CharacterSpecialization
    {
        private readonly ArcherSpecializationConfig _config;

        public ArcherSpecialization(Gender gender, ArcherSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}