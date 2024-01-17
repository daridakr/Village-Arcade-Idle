using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] [Range(1, 100)] private int _maxLevel = 1;
        [SerializeField] private UpgradeMetadata _metadata;
        [SerializeField] private UpgradePriceTable _priceTable;

        private Upgrade _upgrade;

        public string Id => _id;
        public int MaxLevel => _maxLevel;
        public string Title => _metadata.Title;
        public Sprite Icon => _metadata.Icon;
        public UpgradePriceTable PriceTable => _priceTable;
        public Upgrade Upgrade => _upgrade;

        private void OnValidate()
        {
            try
            {
                Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected virtual void Validate()
        {
            _priceTable.OnValidate(_maxLevel);
        }

        public virtual void InstantiateUpgrade(Upgrade upgrade)
        {
            _upgrade = upgrade;
        }
    }
}