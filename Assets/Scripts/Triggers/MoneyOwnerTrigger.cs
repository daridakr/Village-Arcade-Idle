
using UnityEngine;

public class MoneyOwnerTrigger : Trigger<PlayerMoney>
{
    private PlayerMoney _enteredOwner;

    public PlayerMoney Owner => _enteredOwner;

    protected override void OnEnter(PlayerMoney triggered)
    {
        _enteredOwner = triggered;
    }

    protected override void OnExit(PlayerMoney triggered)
    {
        _enteredOwner = null;
    }
}
