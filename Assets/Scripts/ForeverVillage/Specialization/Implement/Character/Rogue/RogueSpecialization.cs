namespace ForeverVillage.Scripts.Character
{
    public class RogueSpecialization : CharacterSpecialization
    {
        private readonly RogueSpecializationConfig _config;

        public RogueSpecialization(Gender gender, RogueSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}