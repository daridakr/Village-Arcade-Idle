using UnityEngine;

namespace ForeverVillage.Scripts
{
    public sealed class UpgradesAssetSupplier
    {
        private UpgradeCatalog _catalog;

        //public UpgradeConfig GetUpgrade(string id)
        //{
        //    return _catalog.FindChest(id);
        //}

        public UpgradeConfig[] GetAllUpgrades()
        {
            return _catalog.GetAllUpgrades();
        }

        //void IAppConfigsLoader.LoadConfigs()
        //{
        //    _catalog = Resources.Load<ChestCatalog>("ChestsCatalog");
        //    Debug.Log($"CatalogChests Task loaded, {_catalog.name}");

        //    //_catalog = Resources.Load<ChestCatalog>(BoosterExtensions.BOOSTER_CATALOG_RESOURCE_PATH);
        //}
    }
}