using Village.Character;

namespace Village
{
    public abstract class CharacterSpecialization : Specialization,
        ICharacterSpecialization
    {
        private string _id => _config.Id;

        private readonly CharacterSpecializationConfig _config;

        protected Gender _gender;

        public string Id => _config.Id;
        public SpecializationConfig Data => _config;

        protected CharacterSpecialization(CharacterSpecializationConfig config) : base(config)
            => _config = config;

        public override string GetModelPath()
        {
            return _gender == Gender.Male ? _config.MalePrefabPath : _config.FemalePrefabPath;
        }
    }
}