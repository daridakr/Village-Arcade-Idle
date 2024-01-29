namespace Village.Character
{
    public class AdventurerSpecialization : CharacterSpecialization
    {
        private readonly AdventurerSpecializationConfig _config;

        public AdventurerSpecialization(Gender gender, AdventurerSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}