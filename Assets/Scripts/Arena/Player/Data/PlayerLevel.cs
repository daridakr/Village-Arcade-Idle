using Village;

namespace Arena
{
    public sealed class PlayerLevel : global::PlayerLevel
    {
        public float _luck;

        public float Luck { get => _luck; }

        protected override void Initialize() => _level = new ExperienceLevel(string.Empty);
    }
}