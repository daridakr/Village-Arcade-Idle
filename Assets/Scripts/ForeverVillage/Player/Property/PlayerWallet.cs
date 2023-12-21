using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent (typeof (PlayerGems))]
    [RequireComponent(typeof(PlayerCoins))]
    public class PlayerWallet : MonoBehaviour
    {
        private PlayerCoins _playerCoins;
        private PlayerGems _playerGems;

        public int Coins => _playerCoins.Balance;
        public int Gems => _playerGems.Balance;

        public void SpendCoins(int value) => _playerCoins.Spend(value);
        public void SpendGems(int value) => _playerGems.Spend(value);

        private void Awake()
        {
            _playerCoins = GetComponent<PlayerCoins>();
            _playerGems = GetComponent<PlayerGems>();
        }
    }
}