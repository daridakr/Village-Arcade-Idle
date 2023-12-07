using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private PlayerLevel _playerLevel;

    public void AddMoney(int money)
    {
        _playerMoney.Get(money);
    }

    public void AddExp(int value)
    {
        _playerLevel.TakeExp(value);
    }
}
