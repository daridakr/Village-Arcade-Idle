using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private MoneyOwner _playerMoney;
    [SerializeField] private PlayerLevel _playerLevel;

    public void AddMoney(int money)
    {
        _playerMoney.AddMoney(money);
    }

    public void AddExp(int value)
    {
        _playerLevel.TakeExp(value);
    }
}
