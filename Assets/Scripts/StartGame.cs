using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private MoneyOwner _playerMoney;

    private void Start()
    {
        _playerMoney.AddMoney(1000);
    }
}
