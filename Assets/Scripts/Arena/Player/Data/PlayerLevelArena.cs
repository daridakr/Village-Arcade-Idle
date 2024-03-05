using ForeverVillage;
using Village;

namespace Arena
{
    public sealed class PlayerLevelArena : PlayerLevelBase
    {
        public float _luck;

        public float Luck { get => _luck; }

        protected override void Initialize() => _level = new ExperienceLevel(string.Empty);
    }
}