namespace ForeverVillage.Scripts.Character
{
    public class RogueSpecialization : Specialization
    {
        private readonly RogueSpecializationConfig _config;

        public RogueSpecialization(RogueSpecializationConfig config) : base(config)
        {
            _config = config;
        }
    }
}