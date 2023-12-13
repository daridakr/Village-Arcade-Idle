using System;
using UnityEngine;

[RequireComponent(typeof(GuidableObject))]
[RequireComponent(typeof(ExperiencePointGiver))]
public class BuildingZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private PlayerTimerCleaner _cleaner; // need somehow divide that class to destroyed bulding, empty etc
    [SerializeField] private PlayerTimerBuilder _builder;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private BuildingZoneView _view; // should remove after save builded buildings implementation and rewrite to like regionZone w events

    private IExperiencePointGiver _experienceGiver;
    private const int _clearPrice = 20;

    private BuildZone _model;
    private GuidableObject _guidable;

    //private Building _building;

    public BuildingZoneState State => _model.State; // should remove after save builded buildings implementation and rewrite to like regionZone w events

    public event Action Cleared;

    //[Inject]
    //public void Construct(PlayerTimerCleaner cleaner, PlayerTimerBuilder builder)
    //{
    //    _cleaner = cleaner;
    //    _builder = builder;
    //}

    //public void ConstructExperience(IExperiencePointGiver experienceGiver)
    //{
    //    _experienceGiver = experienceGiver;
    //}

    private void OnEnable()
    {
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;

        _guidable = GetComponent<GuidableObject>();
        _model = new BuildZone(BuildingZoneState.Destroyed, _guidable.GUID);
        _model.Destroyed += OnDestroyedZone;
        _model.Cleared += OnClearedZone;
        _model.Builded += OnBuildedZone;
        //_zone.Unlocked += OnZonePaid;
        _model.Load();
    }

    private void Awake()
    {
        _experienceGiver = GetComponent<ExperiencePointGiver>();
    }

    private void OnDestroyedZone()
    {
        _model.Destroyed -= OnDestroyedZone;
        _view.CanClear += ClearBuilding;
    }

    // maybe should separate class with abstract method like Interact()? bc these two methods almost the same 
    private void ClearBuilding()
    {
        _view.CanClear -= ClearBuilding;

        PlayerMoney buyer = _moneyOwnerTrigger.Owner;
        buyer.Spend(_clearPrice);

        _cleaner.StartClean(this);
        _model.Clear();

        _cleaner.Stopped += OnCleanStopped;
    }

    private void OnCleanStopped()
    {
        _cleaner.Stopped -= OnCleanStopped;

        _experienceGiver.Give();
    }

    private void OnClearedZone()
    {
        _model.Cleared -= OnClearedZone;

        DestroyedBuilding destroyedBuilding = _buildPoint.GetComponentInChildren<DestroyedBuilding>();
        destroyedBuilding?.Clear();

        Cleared?.Invoke();
        _view.CanBuild += Build;
    }

    private void Build(BuildingData buildingData)
    {
        _view.CanBuild -= Build;

        PlayerMoney buyer = _moneyOwnerTrigger.Owner;
        buyer.Spend(buildingData.Price);

        //_building = buildingData.Renderer;

        _builder.StartBuild(this);
        _model.Build();

        _builder.Stopped += OnBuildStopped;

        //_currentState = BuildingZoneState.Builded;
    }

    private void OnBuildStopped()
    {
        _builder.Stopped -= OnBuildedZone;

        _experienceGiver.Give();
    }

    private void OnBuildedZone()
    {
        _model.Builded -= OnBuildedZone;

        //Instantiate(_building, _buildPoint);
    }

    private void TriggerEnter(PlayerMoney moneyOwner)
    {
        //ShowRewardPlacement?.Invoke(_placement);

        switch (State)
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
    }
}

// should remove after save builded buildings implementation and rewrite to like regionZone w events
[Serializable]
public enum BuildingZoneState
{
    Destroyed,
    Empty,
    Builded
}
