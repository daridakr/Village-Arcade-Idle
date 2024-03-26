using ForeverVillage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class StackableBuilding : Building
    {
        [SerializeField] private int _capacity;

        private List<IItem> _store = new List<IItem>();
        //private RegionData _region; // one in one region

        //public IEnumerable<Item> Store => _store;

        public event Action<int> CapacityUpdated;
        public event Action<int> StoreUpdated;

        private void Start()
        {
            _upgradeMultiplier = 5;
            CapacityUpdated?.Invoke(_capacity);
        }

        private void Stack(IItem item)
        {
            if (_store.Count + 1 >= _capacity)
            {
                return;
            }

            _store.Add(item);
            StoreUpdated?.Invoke(_store.Count);
        }

        private IEnumerable<IItem> Get()
        {
            return _store;
        }

        protected override void Upgrade()
        {
            base.Upgrade();

            _capacity += (int)_upgradeMultiplier;
            CapacityUpdated?.Invoke(_capacity);
        }

        protected override List<int> InitStats()
        {
            List<int> stats = new List<int>
        {
            _capacity
        };

            return stats;
        }
    }
}