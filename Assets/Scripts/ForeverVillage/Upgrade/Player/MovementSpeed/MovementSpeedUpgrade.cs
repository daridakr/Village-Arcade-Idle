using System;
using Zenject;

namespace ForeverVillage.Scripts.Upgrades.Player
{
    public sealed class MovementSpeedUpgrade : Upgrade,
        IInitializable
    {
        private readonly MovementSpeedUpgradeConfig _config;

        public override string CurrentStats => _config.GetSpeedFor(Level).ToString();
        public override string NextImprovement => _config.SpeedStep.ToString();

        public Action<float> Updated;

        public MovementSpeedUpgrade(MovementSpeedUpgradeConfig config) : base(config)
        {
            _config = config;
            _config.InstantiateUpgrade(this);
        }

        public void Initialize()
        {
            var speed = _config.GetSpeedFor(Level);
            Updated?.Invoke(speed);
        }

        protected override void UpdateLevel(int newLevel)
        {
            var speed = _config.GetSpeedFor(newLevel);
            Updated?.Invoke(speed);
        }
    }
}