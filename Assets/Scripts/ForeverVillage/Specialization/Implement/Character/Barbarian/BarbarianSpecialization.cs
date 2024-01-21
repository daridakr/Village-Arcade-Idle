namespace ForeverVillage.Scripts.Character
{
    public class BarbarianSpecialization : Specialization
    {
        private readonly BarbarianSpecializationConfig _config;

        public BarbarianSpecialization(BarbarianSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}