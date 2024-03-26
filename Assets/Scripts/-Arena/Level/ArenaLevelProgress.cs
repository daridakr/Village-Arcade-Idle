using ForeverVillage;
using System;
using Zenject;

namespace Arena
{
    public sealed class ArenaLevelProgress :
        IInitializable,
        IDisposable
    {
        private PlayerCoins _playerCoins;

        private int _earnedCoins = 0;

        [Inject]
        private void Construct(PlayerCoins playerCoins) => _playerCoins = playerCoins;

        public void Initialize() => _playerCoins.Recieved += (value) => _earnedCoins += value;
        public void Dispose() => _playerCoins.Recieved -= (value) => _earnedCoins += value;
    }
}