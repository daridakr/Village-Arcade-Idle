using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class PlayerUpgradeConfig : ScriptableObject
    {
        [SerializeField] private UpgradePriceTable _priceTable;

        public UpgradePriceTable PriceTable => _priceTable;
    }
}