namespace ForeverVillage.Scripts.Character
{
    public sealed class KnightSpecialization : Specialization
    {
        private readonly KnightSpecializationConfig _config;

        public KnightSpecialization(KnightSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}