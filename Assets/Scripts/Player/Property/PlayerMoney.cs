using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    private MoneyBalance _money;

    public int Balance => _money.Value;
    public bool HasMoney => _money.Value > 0;

    public event UnityAction<int> BalanceChanged;

    private void OnEnable()
    {
        _money = new MoneyBalance(SaveKeyParams.Player.MoneyBalance);
        _money.Load();

        _money.ValueChanged += OnBalanceChanged;
    }

    private void OnBalanceChanged()
    {
        BalanceChanged?.Invoke(_money.Value);
        _money.Save();
    }

    public void Get(int value)
    {
        _money.Add(value);

        //_totalMoneyProgress.Add(value);
        //_totalMoneyProgress.Save();
    }

    public void Spend(int value)
    {
        _money.Spend(value);
    }

    private void OnDisable()
    {
        _money.ValueChanged -= OnBalanceChanged;
        _money.Save();
    }
}
