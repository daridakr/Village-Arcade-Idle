using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class UnlockedRegions : KeySavedObject<UnlockedRegions>
{
    [SerializeField] private static Hashtable _savedData = new Hashtable();

    public UnlockedRegions(string key)
        : base(key)
    {
    }

    public static BuyZone GetZone(int cost, string guid)
    {
        if (_savedData.ContainsKey(guid))
            return (BuyZone)_savedData[guid];

        BuyZone buyZone = new BuyZone(cost, guid);
        _savedData.Add(guid, buyZone);

        return buyZone;
    }

    protected override void OnLoad(UnlockedRegions loadedObject)
    {

    }

    //public UnlockedRegionZones(string key) : base(key)
    //{
    //}

    //public static RegionZone GetZone(int totalCost, string guid)
    //{
    //    if (_savedData.ContainsKey(guid))
    //        return (RegionZone)_savedData[guid];

    //    RegionZone regionZone = new RegionZone(totalCost, guid);
    //    _savedData.Add(guid, regionZone);

    //    return regionZone;
    //}

    //protected override void OnLoad(UnlockedRegionZones loadedObject)
    //{
    //    _dynamicCost = loadedObject._dynamicCost;

    //    if (_dynamicCost.CurrentCost == 0)
    //        Unlocked?.Invoke(true);
    //}
}
