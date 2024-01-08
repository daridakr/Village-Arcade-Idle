using UnityEngine;

namespace ForeverVillage.Scripts
{
    [CreateAssetMenu(fileName = "NewUpgradeCatalog", menuName = "Upgrades/Upgrade Catalog")]
    public sealed class UpgradeCatalog : ScriptableObject
    {
        [SerializeField] private UpgradeConfig[] _data;

        public UpgradeConfig[] GetAllUpgrades()
        {
            return _data;
        }

        //public UpgradeConfig FindUpgrade(string id)
        //{
        //    var length = this.configs.Length;
        //    for (var i = 0; i < length; i++)
        //    {
        //        var config = this.configs[i];
        //        if (config.Id == id)
        //        {
        //            return config;
        //        }
        //    }

        //    throw new Exception($"Config with {id} is not found!");
        //}
    }
}