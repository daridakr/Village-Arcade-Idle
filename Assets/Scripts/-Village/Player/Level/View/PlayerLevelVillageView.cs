using Zenject;

namespace Village
{
    public sealed class PlayerLevelVillageView : PlayerLevelView
    {
        [Inject]
        private void Construct(PlayerLevelVillage playerLevel) => Init(playerLevel);
    }
}