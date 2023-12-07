using UnityEngine;

public class RegionLock : MonoBehaviour
{
    [SerializeField] private RegionData _region;
    [SerializeField] private RegionZone _interactionZone;

    private void OnEnable()
    {
        _interactionZone.Unlocked += OnRegionUnlockToBuy;
        _interactionZone.Paid += OnRegionPaid;
    }

    private void Start()
    {
        _region.Hide();
    }

    private void OnRegionUnlockToBuy()
    {
        _interactionZone.Unlocked -= OnRegionUnlockToBuy;
        _interactionZone.InitPrice(_region.Price);
    }

    private void OnRegionPaid()
    {
        _region.Display();
        _interactionZone.Paid -= OnRegionPaid;
        Destroy(_interactionZone.gameObject);
        Destroy(gameObject);
    }
}
