using ForeverVillage;
using Village;

namespace Arena
{
    public sealed class PlayerLevelArena : PlayerLevelBase
    {
        protected override void Initialize() => _level = new ExperienceLevel(string.Empty);
    }
}