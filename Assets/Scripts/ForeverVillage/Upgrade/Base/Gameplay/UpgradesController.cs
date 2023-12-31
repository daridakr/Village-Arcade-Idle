using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ForeverVillage.Scripts
{
    public sealed class UpgradesController : MonoBehaviour,
        IUpgradesController
    {
        [SerializeField][FormerlySerializedAs("assets")] private UpgradeCatalog _catalog;
        [SerializeField] private PlayerWallet _playerWallet;
        [Space][ReadOnly][ShowInInspector] private Dictionary<string, Upgrade> _upgrades;

        private MoneyUpgrader _upgrader;

        private void Awake()
        {
            _upgrader = new MoneyUpgrader(_playerWallet);
            _upgrades = new Dictionary<string, Upgrade>();
            SetupUpgrades();
        }

        public bool TryUpgrade(IUpgrade upgrade)
        {
            return _upgrader.TryUpgrade((Upgrade)upgrade);
        }

        public IUpgrade GetUpgrade(string guid)
        {
            return _upgrades[guid];
        }

        public IUpgrade[] GetAllUpgrades()
        {
            return _upgrades.Values.ToArray();
        }

        private void SetupUpgrades()
        {
            UpgradeConfig[] configs = _catalog.GetAllUpgrades();

            foreach(var config in configs)
            {
                var upgrade = config.Upgrade;
                _upgrades.Add(config.Id, upgrade);
            }
        }
    }
}