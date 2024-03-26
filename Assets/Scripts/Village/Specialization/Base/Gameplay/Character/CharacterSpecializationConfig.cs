namespace Village
{
    public abstract class CharacterSpecializationConfig : SpecializationConfig
    {
        public abstract string MalePrefabPath { get; }
        public abstract string FemalePrefabPath { get; }
    }
}