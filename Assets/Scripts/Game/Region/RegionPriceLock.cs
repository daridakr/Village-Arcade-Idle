using System;
using UnityEngine;

public class RegionPriceLock : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private RegionData _region;
    [SerializeField] private RegionZone _buyZone;

    public int Price => _price;

    public event Action<RegionPriceLock, RegionData> Buyed;

    private void OnEnable()
    {
        _buyZone.Unlocked += OnRegionUnlockToBuy;
        _buyZone.Paid += OnRegionPaid;
    }

    private void Start()
    {
        _region.Hide();
    }

    private void OnRegionUnlockToBuy()
    {
        _buyZone.Unlocked -= OnRegionUnlockToBuy;
        _buyZone.InitPrice(_price);
    }

    private void OnRegionPaid()
    {
        _region.Display();
        _buyZone.Paid -= OnRegionPaid;

        Buyed?.Invoke(this, _region);

        Destroy(_buyZone.gameObject);
        Destroy(gameObject);
    }
}