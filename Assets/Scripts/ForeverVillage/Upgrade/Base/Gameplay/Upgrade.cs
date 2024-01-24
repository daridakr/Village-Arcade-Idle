using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class Upgrade :
        IUpgrade
    {
        [ReadOnly][ShowInInspector] public string Id => _config.Id;
        [ReadOnly][ShowInInspector] public int Level => _currentLevel;
        [ReadOnly][ShowInInspector] public int MaxLevel => _config.MaxLevel;
        [ReadOnly][ShowInInspector] public string Title => _config.Title;
        [ReadOnly][ShowInInspector] public int NextPrice => _config.PriceTable.GetPriceFor(NextLevel);
        [ReadOnly][PreviewField] public Sprite Icon => _config.Icon;

        public abstract string CurrentStats { get; }
        public abstract string NextImprovement { get; }

        public float Progress => (float)_currentLevel / MaxLevel;
        public bool IsMaxLevel => Level == MaxLevel;

        private int NextLevel => _currentLevel + 1;

        public event Action<int> OnLevelUp;

        private readonly UpgradeConfig _config;
        private int _currentLevel = 1;

        public Upgrade(UpgradeConfig config)
        {
            _config = config;
        }

        public void SetupLevel(int value)
        {
            if (value <= 0 || value > MaxLevel)
            {
                throw new ArgumentException($"Level {value} for {Title} is invalid. Max allowable level - {MaxLevel}");
            }

            _currentLevel = value;
        }

        public void IncrementLevel()
        {
            if (_currentLevel >= MaxLevel)
            {
                throw new Exception("Max level is reached!");
            }

            _currentLevel++;
            UpdateLevel(_currentLevel);
            OnLevelUp?.Invoke(_currentLevel);
        }

        protected abstract void UpdateLevel(int newLevel); // templtate pattern
    }
}