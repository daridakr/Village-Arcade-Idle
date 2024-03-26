using Village;
using Zenject;

namespace Arena
{
    public sealed class PlayerLevelArenaView : PlayerLevelView
    {
        [Inject]
        private void Construct(PlayerLevelArena playerLevel) => Init(playerLevel);
    }
}