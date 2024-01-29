using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class RegionZoneData : KeySavedObject<RegionZoneData>
    {
        [SerializeField] private DynamicCost _dynamicCost;

        public int TotalCost => _dynamicCost.Total;
        public int CurrentCost => _dynamicCost.Current;

        public event Action<int> CostUpdated;
        public event Action Unlocked;

        public RegionZoneData(int totalCost, string guid)
            : base(guid)
        {
            _dynamicCost = new DynamicCost(totalCost);
        }

        protected override void OnLoad(RegionZoneData loadedObject)
        {
            _dynamicCost = loadedObject._dynamicCost;

            if (_dynamicCost.Current == 0)
            {
                Unlocked?.Invoke();
            }
            else
            {
                CostUpdated?.Invoke(_dynamicCost.Current);
            }
        }

        public void ReduceCost(int value)
        {
            _dynamicCost.Subtract(value);
            CostUpdated?.Invoke(_dynamicCost.Current);

            if (_dynamicCost.Current == 0)
            {
                Unlocked?.Invoke();
            }
        }
    }
}