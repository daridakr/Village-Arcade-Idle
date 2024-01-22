namespace ForeverVillage.Scripts.Character
{
    public class BarbarianSpecialization : CharacterSpecialization
    {
        private readonly BarbarianSpecializationConfig _config;

        public BarbarianSpecialization(Gender gender, BarbarianSpecializationConfig config) : base(config)
        {
            _config = config;
            _gender = gender;
        }
    }
}