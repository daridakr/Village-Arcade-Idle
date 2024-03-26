using TMPro;
using UnityEngine;
using Zenject;

namespace ForeverVillage
{
    public abstract class PlayerMoneyDisplay<T> : MonoBehaviour where T : PlayerMoney
    {
        [SerializeField] private TMP_Text _moneyTextDisplay;

        private T _playerMoney;

        protected string Current => _playerMoney.Balance.ToString();

        [Inject]
        private void Construct(T playerMoney) => _playerMoney = playerMoney;

        private void OnEnable()
        {
            _playerMoney.Recieved += OnBalanceChanged;
            _playerMoney.Spended += OnBalanceChanged;
        }

        private void Start() => _moneyTextDisplay.text = SetStartedValue();

        private void OnBalanceChanged(int value) =>
            _moneyTextDisplay.text = SetChangedValue(value);

        protected abstract string SetStartedValue();
        protected abstract string SetChangedValue(int value);

        private void OnDisable()
        {
            _playerMoney.Recieved -= OnBalanceChanged;
            _playerMoney.Spended -= OnBalanceChanged;
        }
    }
}