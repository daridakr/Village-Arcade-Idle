using UnityEngine;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private BuildingZoneView _view;

    [SerializeField] private GameObject _building; // to destroy if it's destroyed state
    [SerializeField] private ParticleSystem _effect;

    private BuildingZoneState _currentState;
    private int _price = 20;

    private void OnEnable()
    {
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;

        _view.ClearStarted += ClearZone;
    }

    private void ClearZone()
    {
        _moneyOwnerTrigger.Owner.SpendMoney(_price);
        Destroy(_building);
        _effect.Stop();
        _currentState = BuildingZoneState.Empty;
    }

    private void TriggerEnter(MoneyOwner moneyOwner)
    {
        //ShowRewardPlacement?.Invoke(_placement);

        switch (_currentState)
        {
            case BuildingZoneState.Destroyed:
                _view.ShowClearCanvas(_price, moneyOwner.Balance);
                break;
            case BuildingZoneState.Empty:
                _view.ShowBuildCanvas();
                break;
            case BuildingZoneState.Builded:
                _view.ShowUpgradeCanvas();
                break;
            default:
                break;
        }

        //UpdateView();
    }

    private void TriggerExit(MoneyOwner moneyOwner)
    {
        _view.HideClearCanvas();
    }

    private void OnDisable()
    {
        _moneyOwnerTrigger.Enter -= TriggerEnter;
        _moneyOwnerTrigger.Exit -= TriggerExit;
    }
}

public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
