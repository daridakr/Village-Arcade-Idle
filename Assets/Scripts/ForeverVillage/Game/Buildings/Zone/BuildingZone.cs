using System;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GuidableObject))]
    [RequireComponent(typeof(ExperiencePointGiver))]
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private PlayerCoinsTrigger _playerCoinsTrigger;
        [SerializeField] private PlayerTimerCleaner _cleaner; // need somehow divide that class to destroyed bulding, empty etc
        [SerializeField] private PlayerTimerBuilder _builder;
        [SerializeField] private Transform _buildPoint;
        [SerializeField] private BuildingZoneView _view; // should remove after save builded buildings implementation and rewrite to like regionZone w events

        private ExperiencePointGiver _experienceGiver;
        private const int _expPointsCount = 5;
        private const int _clearPrice = 20;
        private Building _building;
        private SerializableBuldingZone _savedModel;
        private GuidableObject _guidable;
        private IBuildingFactory _buildingFactory;

        public BuildingZoneState State => _savedModel.State; // should remove after save builded buildings implementation and rewrite to like regionZone w events

        public event Action Cleared;
        public event Action Building;
        public event Action Builded;

        [Inject]
        public void Construct(IBuildingFactory buildingFactory)
        {
            _buildingFactory = buildingFactory;
        }

        private void OnEnable()
        {
            _playerCoinsTrigger.Enter += TriggerEnter;
            _playerCoinsTrigger.Exit += TriggerExit;

            _guidable = GetComponent<GuidableObject>();
            _savedModel = new SerializableBuldingZone(BuildingZoneState.Destroyed, _guidable.GUID);
            _savedModel.Destroyed += OnDestroyedZone;
            _savedModel.Cleared += OnClearedZone;
            _savedModel.BuildingLoaded += OnBuildingLoaded;
            _savedModel.Load();
        }

        private void Awake()
        {
            _experienceGiver = GetComponent<ExperiencePointGiver>();
        }

        private void OnDestroyedZone()
        {
            _savedModel.Destroyed -= OnDestroyedZone;
            _view.CanClear += Clear;
        }

        // maybe should separate class with abstract method like Interact()? bc these two methods almost the same 
        private void Clear()
        {
            _view.CanClear -= Clear;

            PlayerCoins buyer = _playerCoinsTrigger.Entered;
            buyer.Spend(_clearPrice);

            _cleaner.StartClean(this);
            _savedModel.Clear();

            _cleaner.Stopped += OnCleanStopped;
        }

        private void OnCleanStopped()
        {
            _cleaner.Stopped -= OnCleanStopped;

            _experienceGiver.Give(_expPointsCount);
        }

        private void OnClearedZone()
        {
            _savedModel.Cleared -= OnClearedZone;

            DestroyedBuilding destroyedBuilding = _buildPoint.GetComponentInChildren<DestroyedBuilding>();
            destroyedBuilding?.Clear();

            Cleared?.Invoke();
            _view.CanBuild += Build;
        }

        private void Build(Building building)
        {
            _view.CanBuild -= Build;

            PlayerCoins buyer = _playerCoinsTrigger.Entered;
            buyer.Spend(building.Price);

            _building = building;

            Building?.Invoke();
            _builder.StartBuild(this);
            _builder.Stopped += OnBuildStopped;
        }

        private void OnBuildStopped()
        {
            _builder.Stopped -= OnBuildStopped;
            Builded?.Invoke();

            SetupBuilding();
            _experienceGiver.Give(_expPointsCount);
        }

        private void SetupBuilding()
        {
            //Building builded = _diContainer.InstantiatePrefabForComponent<Building>(_building, _buildPoint);
            Building builded = _buildingFactory.Create(_building, _buildPoint);
            _savedModel.ZoneBuilded(_building.name, builded.Guid);
        }

        private void OnBuildingLoaded(BuildedBuilding serializedData)
        {
            _savedModel.BuildingLoaded -= OnBuildingLoaded;

            Building buildingPrefab = Resources.Load<Building>(serializedData.Prefab);
            Building builded = _buildingFactory.Create(buildingPrefab, _buildPoint);
            //Building builded = _diContainer.InstantiatePrefabForComponent<Building>(buildingPrefab, _buildPoint);

            Builded?.Invoke();

            // data initialize
            //builded.Instantiate(serializedData);
        }

        private void TriggerEnter(PlayerCoins coins)
        {
            //ShowRewardPlacement?.Invoke(_placement);

            switch (State)
            {
                case BuildingZoneState.Destroyed:
                    _view.ShowClearButton(_clearPrice, coins.Balance);
                    break;
                case BuildingZoneState.Empty:
                    _view.ShowBuildButton();
                    break;
                default:
                    break;
            }

            //UpdateView();
        }

        private void TriggerExit(PlayerCoins coins)
        {
            _view.HideView();
        }

        private void OnDisable()
        {
            _playerCoinsTrigger.Enter -= TriggerEnter;
            _playerCoinsTrigger.Exit -= TriggerExit;
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
}