using UnityEngine;

namespace Village.Upgrades.Building
{
    [CreateAssetMenu(fileName = "ResidentialUpgradeConfig", menuName = "Upgrades/Building/Residential")]
    public sealed class ResidentialUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private ResidentialUpgradeTable _upgradeTable;

        public int GetGemCapacityFor(int level) => _upgradeTable.GetGemCapacity(level);
        public float GetGemRateFor(int level) => _upgradeTable.GetGemRate(level);

        protected override void Validate()
        {
            base.Validate();
            _upgradeTable.OnValidate(MaxLevel);
        }
    }
}