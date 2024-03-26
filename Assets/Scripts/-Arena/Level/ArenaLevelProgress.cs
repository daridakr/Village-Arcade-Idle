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
        private EnemySpawner _enemySpawner;

        private int _earnedCoins = 0;
        private int _killedEnemies = 0;

        public int Coins => _earnedCoins;
        public int Enemies => _killedEnemies;

        [Inject]
        private void Construct(PlayerCoins playerCoins, EnemySpawner enemySpawner)
        {
            _playerCoins = playerCoins;
            _enemySpawner = enemySpawner;
        }

        public void Initialize()
        {
            _playerCoins.Recieved += (value) => _earnedCoins += value;
            _enemySpawner.MonsterKillded += () => _killedEnemies++;
        }

        public void Dispose()
        {
            _playerCoins.Recieved -= (value) => _earnedCoins += value;
            _enemySpawner.MonsterKillded -= () => _killedEnemies++;
        }
    }
}