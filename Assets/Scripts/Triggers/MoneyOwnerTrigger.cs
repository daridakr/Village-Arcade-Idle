public class MoneyOwnerTrigger : Trigger<MoneyOwner>
{
    private MoneyOwner _enteredOwner;

    public MoneyOwner Owner => _enteredOwner;

    protected override void OnEnter(MoneyOwner triggered)
    {
        _enteredOwner = triggered;
    }

    protected override void OnExit(MoneyOwner triggered)
    {
        _enteredOwner = null;
    }
}
