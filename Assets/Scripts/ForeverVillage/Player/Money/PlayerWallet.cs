using UnityEngine;

namespace Village
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField] private PlayerCoins _playerCoins;
        [SerializeField] private PlayerGems _playerGems;

        public int Coins => _playerCoins.Balance;
        public int Gems => _playerGems.Balance;
        public bool IsEmpty => IsCoinsEmpty && IsGemsEmpty;
        public bool IsCoinsEmpty => _playerCoins.IsEmpty;
        public bool IsGemsEmpty => _playerGems.IsEmpty;

        public void SpendCoins(int value) => _playerCoins.Spend(value);
        public void SpendGems(int value) => _playerGems.Spend(value);
        public void RecieveCoins(int value) => _playerCoins.Get(value);
        public void RecieveGems(int value) => _playerGems.Get(value);
    }
}