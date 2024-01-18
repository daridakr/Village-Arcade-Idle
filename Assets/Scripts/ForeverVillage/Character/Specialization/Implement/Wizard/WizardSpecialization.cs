namespace ForeverVillage.Scripts.Character
{
    public class WizardSpecialization : Specialization
    {
        private readonly WizardSpecializationConfig _config;

        public WizardSpecialization(WizardSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}