using UnityEngine;

namespace ForeverVillage.Scripts.Upgrades.Player
{
    [CreateAssetMenu(fileName = "MovementSpeedUpgradeConfig", menuName = "Upgrades/Player/MovementSpeed")]
    public sealed class MovementSpeedUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private MovementSpeedUpgradeTable _speedTable;

        public float SpeedStep => _speedTable.Step;
        public float GetSpeedFor(int level) => _speedTable.GetSpeed(level);

        protected override void Validate()
        {
            base.Validate();
            _speedTable.OnValidate(MaxLevel);
        }
    }
}