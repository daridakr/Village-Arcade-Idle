using Village.Character;

namespace Village
{
    public abstract class CharacterSpecialization : Specialization
    {
        private readonly CharacterSpecializationConfig _config;

        protected Gender _gender;

        protected CharacterSpecialization(CharacterSpecializationConfig config) : base(config)
        {
            _config = config;
        }

        public override string GetModelPath()
        {
            return _gender == Gender.Male ? _config.MalePrefabPath : _config.FemalePrefabPath;
        }
    }
}