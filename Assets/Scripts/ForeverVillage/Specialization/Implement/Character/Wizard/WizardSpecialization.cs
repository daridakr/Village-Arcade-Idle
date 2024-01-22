namespace ForeverVillage.Scripts.Character
{
    public class WizardSpecialization : CharacterSpecialization
    {
        private readonly WizardSpecializationConfig _config;

        public WizardSpecialization(Gender gender, WizardSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}