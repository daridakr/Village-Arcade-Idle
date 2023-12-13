using System;
using System.Collections.Generic;
using UnityEngine;

public class StackableBuilding : Building
{
    [SerializeField] private int _capacity;

    private List<Item> _store = new List<Item>();
    //private RegionData _region; // one in one region

    //public IEnumerable<Item> Store => _store;

    public event Action<int> CapacityUpdated;
    public event Action<int> StoreUpdated;

    private void Start()
    {
        _upgradeMultiplier = 5;
        CapacityUpdated?.Invoke(_capacity);
    }

    private void Stack(Item item)
    {
        if (_store.Count + 1 >= _capacity)
        {
            return;
        }

        _store.Add(item);
        StoreUpdated?.Invoke(_store.Count);
    }

    private IEnumerable<Item> Get()
    {
        return _store;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        _capacity += (int)_upgradeMultiplier;
        CapacityUpdated?.Invoke(_capacity);
    }
}
