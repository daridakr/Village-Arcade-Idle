using UnityEngine;
using Zenject;

namespace Village
{
    public class Test : MonoBehaviour
    {
        private PlayerLevelStorable _playerLevel;
        private PlayerWallet _wallet;

        [Inject]
        private void Construct(PlayerWallet wallet, PlayerLevelStorable playerLevel)
        {
            _playerLevel = playerLevel;
            _wallet = wallet;
        }

        public void AddMoney(int value)
        {
            _wallet.RecieveCoins(value);
        }

        public void AddGems(int count)
        {
            _wallet.RecieveGems(count);
        }

        public void AddExp(int value)
        {
            _playerLevel.TakeExp(value);
        }
    }
}