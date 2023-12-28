using System;

namespace ForeverVillage.Scripts
{
    public abstract class PlayerUpgrade : IPlayerUpgrade
    {
        private int _currentLevel = 1;
        private readonly PlayerUpgradeConfig _config;

        public int Level => _currentLevel;
        public int MaxLevel => throw new NotImplementedException();
        public int NextPrice => _config.PriceTable.GetPriceFor(NextLevel);
        private int NextLevel => _currentLevel + 1;

        public event Action<int> Upgraded;

        public void IncrementLevel()
        {
            if (Level >= MaxLevel)
            {
                throw new Exception("Max level is reached!");
            }

            var nextLevel = Level + 1;
            _currentLevel = nextLevel;
            UpdateLevel(nextLevel);
            Upgraded?.Invoke(nextLevel);
        }

        protected abstract void UpdateLevel(int level);
    }
}