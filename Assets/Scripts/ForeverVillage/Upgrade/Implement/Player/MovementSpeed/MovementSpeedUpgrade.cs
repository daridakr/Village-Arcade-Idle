using System;
using Zenject;

namespace ForeverVillage.Scripts.Upgrades.Player
{
    public sealed class MovementSpeedUpgrade : Upgrade,
        IInitializable
    {
        private readonly MovementSpeedUpgradeConfig _config;

        private float _current => _config.GetSpeedFor(Level);
        public override string CurrentStats => _current.ToString();
        public override string NextImprovement => (_current + _config.SpeedStep).ToString("0.00");

        public Action<float> Updated;

        public MovementSpeedUpgrade(MovementSpeedUpgradeConfig config) : base(config)
        {
            _config = config;
            _config.InstantiateUpgrade(this);
        }

        public void Initialize()
        {
            float speed = _config.GetSpeedFor(Level);
            Updated?.Invoke(speed);
        }

        protected override void UpdateLevel(int newLevel)
        {
            float speed = _config.GetSpeedFor(newLevel);
            Updated?.Invoke(speed);
        }
    }
}