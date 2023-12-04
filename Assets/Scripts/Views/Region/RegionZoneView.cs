using UnityEngine;

public class RegionZoneView : MonoBehaviour
{
    [SerializeField] private Region _region;
    [SerializeField] private LockedRegionCanvas _lockedCanvas;
    [SerializeField] private BuyRegionCanvas _buyCanvas;

    private void OnEnable()
    {
        _region.Locked += DisplayLockedZone;
        _region.Unlocked += DisplayUnlockedZone;
        _region.Buyed += Hide;
    }

    private void DisplayLockedZone(int requiredLevel)
    {
        _lockedCanvas.Display(requiredLevel);
    }

    private void DisplayUnlockedZone(int price)
    {
        _lockedCanvas.Hide();
        _buyCanvas.Display(price);
    }

    private void Hide()
    {
        _buyCanvas.Hide();
    }

    private void OnDisable()
    {
        _region.Locked -= DisplayLockedZone;
        _region.Unlocked -= DisplayUnlockedZone;
        _region.Buyed -= Hide;
    }
}
