using UnityEngine;
using Zenject;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private BuildingCleaner _buildingCleaner; // need somehow divide that class to destroyed bulding, empty etc
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private BuildingZoneView _view;

    private BuildingRenderer _building;
    private BuildingZoneState _currentState;

    private const int _clearPrice = 20;

    public BuildingZoneState State => _currentState;

    [Inject]
    public void Construct(BuildingCleaner cleaner, BuildingBuilder builder)
    {
        _buildingCleaner = cleaner;
        _builder = builder;
    }

    private void OnEnable()
    {
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;

        _view.CanClear += OnClearZone;
        _view.CanBuild += OnBuildZone;
    }


    // maybe should separate class with abstract method like Interact()? bc these two methods almost the same 
    private void OnClearZone()
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(_clearPrice);

        _buildingCleaner.Clean(this);
        _buildingCleaner.Stopped += ClearBuilding;

        _currentState = BuildingZoneState.Empty;
    }

    private void ClearBuilding()
    {
        if (_currentState == BuildingZoneState.Empty)
        {
            DestroyedBuilding destroyedBuilding = _buildPoint.GetComponentInChildren<DestroyedBuilding>();
            destroyedBuilding?.Clear();

            _buildingCleaner.Stopped -= ClearBuilding;
        }
    }

    private void OnBuildZone(BuildingData buildingData)
    {
        MoneyOwner buyer = _moneyOwnerTrigger.Owner;
        buyer.SpendMoney(buildingData.Price);

        _building = buildingData.Renderer;

        _builder.StartBuildIn(this);
        _builder.Stopped += SetBuilding;

        _currentState = BuildingZoneState.Builded;
    }

    private void SetBuilding()
    {
        if (_currentState == BuildingZoneState.Builded && _building != null)
        {
            Instantiate(_building, _buildPoint);
            _builder.Stopped -= SetBuilding;
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
    }
}

public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
