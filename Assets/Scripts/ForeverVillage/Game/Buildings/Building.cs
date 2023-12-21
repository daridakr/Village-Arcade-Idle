using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GuidableObject))]
    public abstract class Building : MonoBehaviour, IStorableObject
    {
        [SerializeField] private SpecificBuildingData _specificData;
        [SerializeField] private BuildingTypeData _typeData;
        [SerializeField] private BuildingLevel[] _levels;
        [SerializeField] private PlayerWalletTrigger _playerWalletTrigger;
        [SerializeField] private BuildingView _commonView;

        [SerializeField] protected int _countInOneRegion;
        [SerializeField] protected int _maxLevel;

        private List<int> _stats;

        //need unique guid for building and also guid for builded building
        private GuidableObject _guidable;

        private int _levelNumber = 0;
        private BuildingLevel _currentLevel;
        protected float _upgradeMultiplier;
        // unlock type (blueprint, quest, level)

        public string Guid => _guidable.GUID;

        public string Description => _typeData.Description;
        public string Name => _specificData.Name;
        public int Price => _specificData.Price;
        public Sprite Icon => _specificData.MainIcon;
        public BuildingLevel NextLevel => _levels[_levelNumber + 1];
        public bool CanUpgrade => _levelNumber + 1 < _maxLevel;

        [Inject]
        public void Construct(UpgradeBuildingPanel upgradePanel)
        {
            _commonView.Init(this, upgradePanel);
        }

        private void OnEnable()
        {
            _guidable = GetComponent<GuidableObject>();
            _guidable.RegenerateGUID();

            _playerWalletTrigger.Enter += OnPlayerWalletEnter;
            _playerWalletTrigger.Exit += OnPlayerWalletExit;

            _stats = InitStats();
            InitLevels();
            RenderLevel();
        }

        private void OnPlayerWalletEnter(PlayerWallet playerWallet)
        {
            if (CanUpgrade)
            {
                _commonView.ShowView();
                _commonView.EarnedNextLevel += Upgrade;
            }
        }

        protected virtual void Upgrade()
        {
            _levelNumber++;
            RenderLevel();

            PlayerWallet playerWallet = _playerWalletTrigger.Entered;

            playerWallet.SpendCoins(_currentLevel.Price);
            playerWallet.SpendGems(_currentLevel.GemsPrice);
        }

        private void OnPlayerWalletExit(PlayerWallet wallet)
        {
            _commonView.HideView();
            _commonView.EarnedNextLevel -= Upgrade;
        }

        private void RenderLevel()
        {
            _currentLevel = _levels[_levelNumber];

            foreach (BuildingLevel level in _levels)
            {
                level.gameObject.SetActive(level != _currentLevel ? false : true);
            }
        }

        private void InitLevels()
        {
            if (_specificData.Icons.Count() != _levels.Length)
            {
                throw new Exception("Don't enought building icons for levels");
            }

            int levelIndex = 0;

            foreach (Sprite icon in _specificData.Icons)
            {
                _levels[levelIndex].Init(Name, icon);
                levelIndex++;
            }
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public Dictionary<Sprite, int> GetStatesWithIcons()
        {
            if (!gameObject.activeInHierarchy)
            {
                _stats = InitStats();
            }

            if (_stats.Count != _typeData.StatIcons.Count())
            {
                return null;
            }

            var statsWithIcons = new Dictionary<Sprite, int>();
            int statNumber = 0;

            foreach (var icon in _typeData.StatIcons)
            {
                statsWithIcons.Add(icon, _stats[statNumber]);
                statNumber++;
            }

            return statsWithIcons;
        }

        protected abstract List<int> InitStats();

        private void OnDisable()
        {
            _playerWalletTrigger.Enter -= OnPlayerWalletEnter;
            _playerWalletTrigger.Exit -= OnPlayerWalletExit;
        }
    }
}