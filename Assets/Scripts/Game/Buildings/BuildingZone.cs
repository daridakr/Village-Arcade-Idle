using System;
using UnityEngine;
using Zenject;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private PlayerTimerCleaner _buildingCleaner; // need somehow divide that class to destroyed bulding, empty etc
    [SerializeField] private PlayerTimerBuilder _builder;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private BuildingZoneView _view; // should remove after save builded buildings implementation and rewrite to like regionZone w events

    private BuildingRenderer _building;
    private BuildingZoneState _currentState; // should remove after save builded buildings implementation and rewrite to like regionZone w events
    private IExperiencePointGiver _experienceGiver;

    private const int _clearPrice = 20;

    public BuildingZoneState State => _currentState; // should remove after save builded buildings implementation and rewrite to like regionZone w events

    public event Action Destroyed;
    public event Action Cleared;
    public event Action Builded;

    [Inject]
    public void Construct(PlayerTimerCleaner cleaner, PlayerTimerBuilder builder)
    {
        _buildingCleaner = cleaner;
        _builder = builder;
    }

    public void ConstructExperience(IExperiencePointGiver experienceGiver)
    {
        _experienceGiver = experienceGiver;
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
        PlayerMoney buyer = _moneyOwnerTrigger.Owner;
        buyer.Spend(_clearPrice);

        _buildingCleaner.StartClean(this);
        _buildingCleaner.Stopped += ClearBuilding;

        _currentState = BuildingZoneState.Empty;
    }

    private void ClearBuilding()
    {
        if (_currentState == BuildingZoneState.Empty)
        {
            DestroyedBuilding destroyedBuilding = _buildPoint.GetComponentInChildren<DestroyedBuilding>();
            destroyedBuilding?.Clear();
            _experienceGiver.Give();

            _buildingCleaner.Stopped -= ClearBuilding;
        }
    }

    private void OnBuildZone(BuildingData buildingData)
    {
        PlayerMoney buyer = _moneyOwnerTrigger.Owner;
        buyer.Spend(buildingData.Price);

        _building = buildingData.Renderer;

        _builder.StartBuild(this);
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

    private void TriggerEnter(PlayerMoney moneyOwner)
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

    private void TriggerExit(PlayerMoney moneyOwner)
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

// should remove after save builded buildings implementation and rewrite to like regionZone w events
public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
