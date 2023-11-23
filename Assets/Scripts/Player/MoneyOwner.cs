using UnityEngine;
using UnityEngine.Events;

public class MoneyOwner : MonoBehaviour
{
    private MoneyBalance _money;

    public int Balance => _money.Value;

    public event UnityAction<int> BalanceChanged;

    private void OnEnable()
    {
        _money = new MoneyBalance();
        _money.Load();

        _money.ValueChanged += OnBalanceChanged;
    }

    private void OnBalanceChanged()
    {
        BalanceChanged?.Invoke(_money.Value);
        _money.Save();
    }

    public void AddMoney(int value)
    {
        _money.Add(value);

        //_totalMoneyProgress.Add(value);
        //_totalMoneyProgress.Save();
    }

    public void SpendMoney(int value)
    {
        _money.Spend(value);
    }


    private void OnDisable()
    {
        _money.ValueChanged -= OnBalanceChanged;
        _money.Save();
    }
}
