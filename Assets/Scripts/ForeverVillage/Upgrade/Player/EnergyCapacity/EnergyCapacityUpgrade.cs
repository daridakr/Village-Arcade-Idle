using UnityEngine;

namespace ForeverVillage.Scripts.Upgrades.Player
{
    public sealed class EnergyCapacityUpgrade : Upgrade
    {
        public EnergyCapacityUpgrade(UpgradeConfig config) : base(config)
        {
        }

        public override string CurrentStats => throw new System.NotImplementedException();

        public override string NextImprovement => throw new System.NotImplementedException();

        protected override void UpdateLevel(int newLevel)
        {
            throw new System.NotImplementedException();
        }
    }
}

