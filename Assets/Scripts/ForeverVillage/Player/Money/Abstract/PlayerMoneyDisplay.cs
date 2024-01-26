using TMPro;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public abstract class PlayerMoneyDisplay<T> : MonoBehaviour where T : PlayerMoney
    {
        [SerializeField] private TMP_Text _moneyTextDisplay;

        private T _playerMoney;

        [Inject]
        protected virtual void Construct(T playerMoney)
        {
            _playerMoney = playerMoney;
        }

        private void OnEnable()
        {
            _playerMoney.BalanceChanged += OnBalanceChanged;
        }

        private void Start()
        {
            _moneyTextDisplay.text = _playerMoney.Balance.ToString();
        }

        private void OnBalanceChanged(int balance)
        {
            _moneyTextDisplay.text = balance.ToString();
        }

        private void OnDisable()
        {
            _playerMoney.BalanceChanged -= OnBalanceChanged;
        }
    }
}