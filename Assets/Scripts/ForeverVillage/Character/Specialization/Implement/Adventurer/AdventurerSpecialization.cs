namespace ForeverVillage.Scripts.Character
{
    public class AdventurerSpecialization : Specialization
    {
        private readonly AdventurerSpecializationConfig _config;

        public AdventurerSpecialization(AdventurerSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}