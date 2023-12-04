using UnityEngine;

public class RegionZoneView : MonoBehaviour
{
    [SerializeField] private RegionZone _zone;
    [SerializeField] private LockedRegionCanvas _lockedCanvas;
    [SerializeField] private BuyRegionCanvas _buyCanvas;

    private void OnEnable()
    {
        _zone.Locked += DisplayLockedZone;
        _zone.PriceUpdated += DisplayUnlockedZone;
        _zone.Buyed += OnRegionBuyed;
    }

    private void DisplayLockedZone(int requiredLevel)
    {
        _lockedCanvas.Display(requiredLevel);
    }

    private void DisplayUnlockedZone(int price)
    {
        _buyCanvas.Display(price);

        if (_lockedCanvas.IsVisible)
        {
            _lockedCanvas.Hide();
        }
    }

    private void OnRegionBuyed()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _zone.Locked -= DisplayLockedZone;
        _zone.PriceUpdated -= DisplayUnlockedZone;
        _zone.Buyed -= OnRegionBuyed;
    }
}
