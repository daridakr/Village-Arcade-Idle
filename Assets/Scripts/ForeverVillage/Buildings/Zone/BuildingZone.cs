using ForeverVillage;
using System;
using UnityEngine;
using Zenject;

namespace Village
{
    [RequireComponent(typeof(GuidableObject))]
    [RequireComponent(typeof(ExperiencePointGiver))]
    public class BuildingZone : MonoBehaviour
    {
        [SerializeField] private PlayerWalletTrigger _playerWalletTrigger;
        [SerializeField] private Transform _buildPoint;
        [SerializeField] private BuildingZoneView _view; // should remove after save builded buildings implementation and rewrite to like regionZone w events
        [SerializeField] private GameObject _clearableTrash;

        #region Gameplay
        private ExperiencePointGiver _experienceGiver;
        private PlayerTimerCleaner _cleaner;
        private PlayerTimerBuilder _builder;
        private GameObject _trash;
        #endregion
        #region Model
        private Building _building;
        private IBuildingFactory _buildingFactory;
        private BuldingZoneData _savedModel;
        private GuidableObject _guidable;
        #endregion Model

        private const int _clearPrice = 20;
        private const int _expPointsCount = 5;

        public BuildingZoneState State => _savedModel.State; // should remove after save builded buildings implementation and rewrite to like regionZone w events

        public event Action Cleared;
        public event Action Building;
        public event Action Builded;

        [Inject]
        private void Construct(IBuildingFactory buildingFactory, PlayerTimerCleaner cleaner, PlayerTimerBuilder builder)
        {
            _buildingFactory = buildingFactory;
            _cleaner = cleaner;
            _builder = builder;
        }

        private void OnEnable()
        {
            _playerWalletTrigger.Enter += TriggerEnter;
            _playerWalletTrigger.Exit += TriggerExit;

            _guidable = GetComponent<GuidableObject>();
            _savedModel = new BuldingZoneData(BuildingZoneState.Destroyed, _guidable.GUID);
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
            _trash = Instantiate(_clearableTrash, _buildPoint);
            _view.CanClear += Clear;
        }

        // maybe should separate class with abstract method like Interact()? bc these two methods almost the same 
        private void Clear()
        {
            _view.CanClear -= Clear;

            PlayerWallet buyer = _playerWalletTrigger.Entered;
            buyer.SpendCoins(_clearPrice);

            _cleaner.StartClean(this);
            _cleaner.Stopped += OnCleanStopped;
        }

        private void OnCleanStopped()
        {
            _cleaner.Stopped -= OnCleanStopped;

            _savedModel.Clear();
            _experienceGiver.Give(_expPointsCount);
        }

        private void OnClearedZone()
        {
            _savedModel.Cleared -= OnClearedZone;

            Destroy(_trash.gameObject);
            Cleared?.Invoke();

            _view.CanBuild += Build;
        }

        private void Build(Building building)
        {
            _view.CanBuild -= Build;

            PlayerWallet buyer = _playerWalletTrigger.Entered;
            buyer.SpendCoins(building.Price);

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
            Building builded = _buildingFactory.Create(_building, _buildPoint);
            _savedModel.ZoneBuilded(_building.name, builded.Guid);
        }

        private void OnBuildingLoaded(BuildingData serializedData)
        {
            _savedModel.BuildingLoaded -= OnBuildingLoaded;

            Building buildingPrefab = Resources.Load<Building>(serializedData.Prefab);
            Building builded = _buildingFactory.Create(buildingPrefab, _buildPoint);

            Builded?.Invoke();

            // data initialize
            //builded.Instantiate(serializedData);
        }

        private void TriggerEnter(PlayerWallet wallet)
        {
            //ShowRewardPlacement?.Invoke(_placement);

            switch (State)
            {
                case BuildingZoneState.Destroyed:
                    _view.ShowClearButton(_clearPrice, wallet.Coins);
                    break;
                case BuildingZoneState.Empty:
                    _view.ShowBuildButton();
                    break;
                default:
                    break;
            }

            //UpdateView();
        }

        private void TriggerExit(PlayerWallet wallet)
        {
            _view.HideView();
        }

        private void OnDisable()
        {
            _playerWalletTrigger.Enter -= TriggerEnter;
            _playerWalletTrigger.Exit -= TriggerExit;
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