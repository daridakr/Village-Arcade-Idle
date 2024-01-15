using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class Test : MonoBehaviour
    {
        private PlayerLevel _playerLevel;
        private PlayerWallet _wallet;

        [Inject]
        public void Construct(PlayerWallet wallet, PlayerLevel playerLevel)
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