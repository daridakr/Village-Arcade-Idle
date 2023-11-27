using UnityEngine;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private BuildingCleaner _buildingCleaner; // need somehow divide that class to destroyed bulding, empty etc
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private BuildingZoneView _view;

    private Building _building;
    private BuildingZoneState _currentState;
    private int _clearPrice = 20;

    public BuildingZoneState State => _currentState;

    private void OnEnable()
    {
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;

        _view.CanClear += OnClearZone;
        _view.CanBuild += OnBuildZone;

        _builder.Stopped += Build;

        //_buildingPoint = _destroyed.transform;
    }


    // maybe should separate class with abstract method like Interact()? bc these two methods almost the same 
    private void OnClearZone()
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(_clearPrice);

        _buildingCleaner.Clean(this);

        DestroyedBuilding destroyedBuilding = _buildPoint.GetComponentInChildren<DestroyedBuilding>();
        destroyedBuilding.Clear();

        _currentState = BuildingZoneState.Empty;
    }

    private void OnBuildZone(BuildingData buildingData)
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(buildingData.Price);

        _builder.StartBuildIn(this);
        //_building.Init(buildingData);

        _currentState = BuildingZoneState.Builded;
    }

    private void Build()
    {
        if (_currentState == BuildingZoneState.Builded && _building != null)
        {
            Building building = Instantiate(_building, _buildPoint);
        }
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

        _view.CanClear -= OnClearZone;
        _view.CanBuild -= OnBuildZone;

        _builder.Stopped -= Build;
    }
}

public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
