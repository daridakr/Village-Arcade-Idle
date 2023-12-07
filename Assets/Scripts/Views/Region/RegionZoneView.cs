using UnityEngine;

public class RegionZoneView : MonoBehaviour
{
    [SerializeField] private RegionZone _zone;
    [SerializeField] private TextDisplayCanvas _lockedCanvas;
    [SerializeField] private TextDisplayCanvas _buyCanvas;

    private void OnEnable()
    {
        _zone.Locked += DisplayLockedZone;
        _zone.Updated += DisplayAvailableZone;
        _zone.Paid += OnRegionZonePaid;
    }

    private void DisplayLockedZone(int condition)
    {
        _lockedCanvas.Display($"Level {condition}");
    }

    private void DisplayAvailableZone(int price)
    {
        _buyCanvas.Display(price.ToString());

        if (_lockedCanvas.IsVisible)
        {
            _lockedCanvas.Hide();
        }
    }

    private void OnRegionZonePaid()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _zone.Locked -= DisplayLockedZone;
        _zone.Updated -= DisplayAvailableZone;
        _zone.Paid -= OnRegionZonePaid;
    }
}
