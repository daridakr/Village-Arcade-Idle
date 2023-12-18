using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private PlayerCoins _playerCoins;
        [SerializeField] private PlayerGems _playerGems;
        [SerializeField] private PlayerLevel _playerLevel;

        public void AddMoney(int value)
        {
            _playerCoins.Get(value);
        }

        public void AddGems(int count)
        {
            _playerGems.Get(count);
        }

        public void AddExp(int value)
        {
            _playerLevel.TakeExp(value);
        }
    }
}