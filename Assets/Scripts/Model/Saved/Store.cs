using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Store : KeySavedObject<Store>
{
    [SerializeField] List<Building> _availableBuildings;

    public event Action Added;

    public Store(string guid)
    : base(guid)
    {

    }

    public void Add(Building building)
    {
        if (building == null)
        {
            throw new ArgumentOutOfRangeException(nameof(building));
        }

        _availableBuildings.Add(building);
        Added?.Invoke();
    }

    protected override void OnLoad(Store loadedObject)
    {
        throw new NotImplementedException();
    }
}
