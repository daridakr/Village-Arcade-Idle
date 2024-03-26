using System;
using Zenject;

namespace Village.Upgrades.Building
{
    public sealed class ResidentialUpgrade : Upgrade,
        IInitializable
    {
        private readonly ResidentialUpgradeConfig _config;

        private float _currentGemCapacity => _config.GetGemCapacityFor(Level);
        public override string CurrentStats => _currentGemCapacity.ToString();
        public override string NextImprovement => _config.GetGemCapacityFor(Level + 1).ToString();

        public Action<int, float> Updated;

        public ResidentialUpgrade(ResidentialUpgradeConfig config) : base(config)
        {
            _config = config;
            _config.InstantiateUpgrade(this);
        }

        public void Initialize()
        {
            int gemsCapacity = _config.GetGemCapacityFor(Level);
            float gemRate = _config.GetGemRateFor(Level);

            Updated?.Invoke(gemsCapacity, gemRate);
        }

        protected override void UpdateLevel(int newLevel)
        {
            int gemsCapacity = _config.GetGemCapacityFor(newLevel);
            float gemRate = _config.GetGemRateFor(newLevel);

            Updated?.Invoke(gemsCapacity, gemRate);
        }
    }
}