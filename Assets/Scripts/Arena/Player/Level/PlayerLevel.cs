using Village;

namespace Vampire
{
    public sealed class PlayerLevel : global::PlayerLevel
    {
        protected override void Initialize() => _level = new ExperienceLevel(string.Empty);
    }
}