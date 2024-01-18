namespace ForeverVillage.Scripts.Character
{
    public class ArcherSpecialization : Specialization
    {
        private readonly ArcherSpecializationConfig _config;

        public ArcherSpecialization(ArcherSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}