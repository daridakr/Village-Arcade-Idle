using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private TMP_Text _moneyTextDisplay;

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
