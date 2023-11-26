using UnityEngine;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private BuildingCleaner _buildingCleaner; // need somehow divide that class to destroyed bulding, empty etc
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private BuildingZoneView _view;

    [SerializeField] private GameObject _building; // to destroy if it's destroyed state
    [SerializeField] private ParticleSystem _effect;

    private BuildingZoneState _currentState;
    private int _clearPrice = 20;

    public BuildingZoneState State => _currentState;

    private void OnEnable()
    {
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;

        _view.ClearStarted += ClearZone;
        _view.BuildStarted += BuildZone;
    }

    private void ClearZone()
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(_clearPrice);

        _buildingCleaner.Clean(this);

        Destroy(_building);
        _effect.Stop();
        _currentState = BuildingZoneState.Empty;
    }

    private void BuildZone(Building building)
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(building.Price);

        _builder.Build(this);
        Instantiate(building);

        _currentState = BuildingZoneState.Builded;
    }

    private void TriggerEnter(MoneyOwner moneyOwner)
    {
        //ShowRewardPlacement?.Invoke(_placement);

        switch (_currentState)
        {
            case BuildingZoneState.Destroyed:
                _view.ShowClearCanvas(_clearPrice, moneyOwner.Balance);
                break;
            case BuildingZoneState.Empty:
                _view.ShowBuildCanvas();
                break;
            case BuildingZoneState.Builded:
                //_view.ShowUpgradeCanvas();
                break;
            default:
                break;
        }

        //UpdateView();
    }

    private void TriggerExit(MoneyOwner moneyOwner)
    {
        _view.HideCanvas();
    }

    private void OnDisable()
    {
        _moneyOwnerTrigger.Enter -= TriggerEnter;
        _moneyOwnerTrigger.Exit -= TriggerExit;

        _view.ClearStarted -= ClearZone;
        _view.BuildStarted -= BuildZone;
    }
}

public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
