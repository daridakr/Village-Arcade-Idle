using UnityEngine;

public class RegionZoneView : MonoBehaviour
{
    [SerializeField] private RegionZone _zone;
    [SerializeField] private TextDisplayCanvas _lockedCanvas;
    [SerializeField] private TextDisplayCanvas _buyCanvas;

    private void OnEnable()
    {
        _zone.Locked += DisplayLockedZone;
        _zone.PriceUpdated += DisplayUnlockedZone;
        _zone.Buyed += OnRegionBuyed;
    }

    private void DisplayLockedZone(int required)
    {
        _lockedCanvas.Display($"Level {required}");
    }

    private void DisplayUnlockedZone(int price)
    {
        _buyCanvas.Display(price.ToString());

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
